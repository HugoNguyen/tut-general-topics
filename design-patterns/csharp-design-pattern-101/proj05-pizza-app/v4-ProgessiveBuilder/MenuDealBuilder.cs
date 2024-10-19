namespace proj05_pizza_app.v4_ProgessiveBuilder
{
    public class MenuDealBuilder
    {
        private readonly MenuDeal menuDeal;

        public MenuDealBuilder()
        {
            menuDeal = new MenuDeal();
        }

        public SideDishBuilder AddSideDish()
        {
            return new SideDishBuilder(this, menuDeal);
        }
    }
}
