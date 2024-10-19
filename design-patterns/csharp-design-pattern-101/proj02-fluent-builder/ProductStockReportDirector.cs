namespace proj02_fluent_builder
{
    public class ProductStockReportDirector
    {
        private readonly IProductStockReportBuilder _productStockReportBuilder;

        public ProductStockReportDirector(IProductStockReportBuilder productStockReportBuilder)
        {
            _productStockReportBuilder = productStockReportBuilder;
        }

        public void BuildStockReport()
        {
            _productStockReportBuilder
                .BuildHeader()
                .BuildBody()
                .BuildFooter();
        }
    }
}
