import { jsPDF } from "jspdf";

export function GeneratePdf(invoice,orderId) {
    const doc = new jsPDF();

    doc.text(invoice.title, 50, 10);


    var generateData = function() {
        var result = [];
        invoice.items.forEach(element => {
            result.push({Equipment:element.itemName, Price:element.itemPrice.toString()+"€"})
    });

    return result;
    };

    function createHeaders(keys) {
        var result = [];
        for (var i = 0; i < keys.length; i += 1) {
            result.push({
            id: keys[i],
            name: keys[i],
            prompt: keys[i],
            width: 100,
            align: "center",
            padding: 0
            });
        }
        return result;
    }

    var headers = createHeaders([
    "Equipment",
    "Price"
    ]);

    doc.table(25, 30, generateData(), headers, { autoSize: false });
    doc.text(`Total price: ${invoice.totalPrice}€. Bonus points earned: ${invoice.bonusPointsEarned}`, 100, 60+invoice.items.length*10);

    doc.save(`Invoice ${orderId}.pdf`);
}


