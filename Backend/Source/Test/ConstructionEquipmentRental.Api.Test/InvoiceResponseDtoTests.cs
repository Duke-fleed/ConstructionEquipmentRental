using System.Linq;
using AutoFixture.NUnit3;
using ConstructionEquipmentRental.Api.Features.Invoice;
using ConstructionEquipmentRental.Services.Models;
using NUnit.Framework;

namespace ConstructionEquipmentRental.Api.Test
{
    public class InvoiceResponseDtoTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test,AutoData]
        public void it_should_correctly_translate_domain_object(InvoiceResponse invoiceResponse)
        {
            var result = InvoiceResponseDto.FromDomain(invoiceResponse);

            Assert.AreEqual(invoiceResponse.BonusPointsEarned, result.BonusPointsEarned);
            Assert.AreEqual(invoiceResponse.Items.Count(), result.Items.Count());
            Assert.AreEqual(invoiceResponse.Items.Sum(x=>x.price), result.TotalPrice);
        }
    }
}