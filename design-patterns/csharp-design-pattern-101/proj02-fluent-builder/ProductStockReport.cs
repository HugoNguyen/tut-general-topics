using System.Text;

namespace proj02_fluent_builder
{
    public class ProductStockReport
    {
        public string HeaderPart { get; set; }
        public string BodyPart { get; set; }
        public string FooterPart { get; set; }
        public override string ToString() =>
            new StringBuilder()
            .AppendLine(HeaderPart)
            .AppendLine(BodyPart)
            .AppendLine(FooterPart)
            .ToString();
    }
}
