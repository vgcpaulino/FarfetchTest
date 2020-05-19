using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumDotNetTest.Helpers
{
    public class UrlLinks
    {
        
        public string HomeUrl => "https://www.farfetch.com/br/";
        public string LoginUrl => $"{HomeUrl}login.aspx";
        private string ShoppingUrl => $"{HomeUrl}shopping/";
        private string ShoppingMenUrl => $"{ShoppingUrl}men/";
        public string MenCloathingUrl => $"{ShoppingMenUrl}clothing-2/items.aspx";
        //public string ProductTShirtUrl => $"{ShoppingMenUrl}saint-laurent-camiseta-com-estampa-de-logo-item-12997588.aspx?storeid=10814";
        public string ProductTShirtUrl => $"{ShoppingMenUrl}saint-laurent-camiseta-estampada-item-14146936.aspx?storeid=9462";
        public string ProductTShirtDiscountUrl => $"{ShoppingMenUrl}saint-laurent-camiseta-mangas-curtas-item-13510622.aspx?storeid=10704";
        public string ProductModelUrl => $"{ShoppingMenUrl}balenciaga-tenis-triple-s-item-12967005.aspx?storeid=10952";

    }
}
