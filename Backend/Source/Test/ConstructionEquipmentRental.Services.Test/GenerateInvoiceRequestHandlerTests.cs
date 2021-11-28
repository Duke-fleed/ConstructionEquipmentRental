using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ConstructionEquipmentRental.Services.GenerateInvoice;
using ConstructionEquipmentRental.Services.Models;
using ConstructionEquipmentRental.Services.Options;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace ConstructionEquipmentRental.Services.Test
{
    [TestFixture]
    public class GenerateInvoiceRequestHandlerTests
    {
        private readonly Mock<IPersistRental> persistRentalMock = new();
        private readonly Mock<IRentalEquipmentInventory> rentalEquipmentInventoryMock = new();
        private GenerateInvoiceRequestHandler generateInvoiceRequestHandler;

        [SetUp]
        public void Setup()
        {
            var testFeeOptions = new FeeOptionsEur()
            {
                OneTime = 100,
                PremiumPerDay = 50,
                RegularPerDay = 25
            };

            generateInvoiceRequestHandler = new GenerateInvoiceRequestHandler(persistRentalMock.Object,
                rentalEquipmentInventoryMock.Object, Microsoft.Extensions.Options.Options.Create(testFeeOptions));
        }

        [Test]
        public async Task it_should_return_null_if_rental_doesnt_exist()
        {
            //Setup
            persistRentalMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(default(Rental));

            //Act
            var request = new GenerateInvoiceRequest(It.IsAny<int>());
            var response = await generateInvoiceRequestHandler.Handle(request, CancellationToken.None);

            //Assert
            Assert.AreEqual(null, response);
        }

        [TestCase(1,2,3)]
        [TestCase(1,  3,  3)]
        [TestCase(1,  4,  4)]
        [TestCase(1, 4,  4)]
        [TestCase(2, 3, 2)]
        public async Task it_should_calculate_bonus_points_correctly(int firstEquipmentId, int secondEquipmentId, int expectedPoints)
        {
            //Setup
            rentalEquipmentInventoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<RentalEquipmentItem>
            {
                new(1, "Item1", EquipmentType.Heavy),
                new(2, "Item2", EquipmentType.Regular),
                new(3, "Item3", EquipmentType.Specialized),
                new(4, "Item4", EquipmentType.Heavy)
            });

            persistRentalMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new Rental
            {
                RentalItems = new List<RentalItem>
                {
                    new()
                    {
                        EquipmentItemId = firstEquipmentId,
                        NumberOfDays = It.IsAny<int>()
                    },
                    new()
                    {
                        EquipmentItemId = secondEquipmentId,
                        NumberOfDays = It.IsAny<int>()
                    }
                }
            });

            //Act
            var request = new GenerateInvoiceRequest(It.IsAny<int>());
            var response = await generateInvoiceRequestHandler.Handle(request, CancellationToken.None);

            //Assert
            Assert.AreEqual(expectedPoints, response.Value.BonusPointsEarned);
        }

        [TestCase(1, 2, 200, 2, 3, 225)]
        [TestCase(1, 1, 150, 3, 3, 150)]
        [TestCase(2, 2, 200, 3, 4, 175)]
        public async Task it_should_calculate_prices_correctly(int firstEquipmentId, int firstEquipmentDays, int firstItemExpectedPrice, int secondEquipmentId, int secondEquipmentDays, int secondItemExpectedPrice)
        {
            //Setup
            var inventory = new List<RentalEquipmentItem>
            {
                new(1, "Item1", EquipmentType.Heavy),
                new(2, "Item2", EquipmentType.Regular),
                new(3, "Item3", EquipmentType.Specialized),
                new(4, "Item4", EquipmentType.Heavy)
            };
            rentalEquipmentInventoryMock.Setup(x => x.GetAll()).ReturnsAsync(inventory);

            persistRentalMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new Rental
            {
                RentalItems = new List<RentalItem>
                {
                    new()
                    {
                        EquipmentItemId = firstEquipmentId,
                        NumberOfDays = firstEquipmentDays
                    },
                    new()
                    {
                        EquipmentItemId = secondEquipmentId,
                        NumberOfDays = secondEquipmentDays
                    }
                }
            });

            

            //Act
            var request = new GenerateInvoiceRequest(It.IsAny<int>());
            var response = await generateInvoiceRequestHandler.Handle(request, CancellationToken.None);

            //Assert
            var firstItemName = inventory.First(x=>x.Id == firstEquipmentId).Name;
            var secondItemName = inventory.First(x => x.Id == secondEquipmentId).Name;
            Assert.AreEqual(firstItemExpectedPrice, response.Value.Items.First(x=>x.name == firstItemName).price);
            Assert.AreEqual(secondItemExpectedPrice, response.Value.Items.First(x => x.name == secondItemName).price);
        }
    }
}