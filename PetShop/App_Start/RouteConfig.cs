using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PetShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "GioiThieu",
              url: "gioi-thieu",
              defaults: new { controller = "Home", action = "Info", id = UrlParameter.Optional },
              namespaces: new[] { "PetShop.Controllers" }
          );

            routes.MapRoute(
              name: "LienHe",
              url: "lien-he",
              defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
              namespaces: new[] { "PetShop.Controllers" }
          );

            routes.MapRoute(
             name: "DichVu",
             url: "dich-vu",
             defaults: new { controller = "Home", action = "Service", id = UrlParameter.Optional },
             namespaces: new[] { "PetShop.Controllers" }
         );

            routes.MapRoute(
            name: "ThanhToan",
            url: "phuong-thuc-thanh-toan",
            defaults: new { controller = "Home", action = "InfoPayment", id = UrlParameter.Optional },
            namespaces: new[] { "PetShop.Controllers" }
        );

            routes.MapRoute(
            name: "DoiTra",
            url: "chinh-sach-doi-tra",
            defaults: new { controller = "Home", action = "ProductReturn", id = UrlParameter.Optional },
            namespaces: new[] { "PetShop.Controllers" }
        );

            routes.MapRoute(
           name: "GIaoHang",
           url: "chinh-sach-giao-hang",
           defaults: new { controller = "Home", action = "PaymentPolicy", id = UrlParameter.Optional },
           namespaces: new[] { "PetShop.Controllers" }
       );

            routes.MapRoute(
               name: "ShopCho",
               url: "san-pham",
               defaults: new { controller = "Shopping", action = "ShopForDog", id = UrlParameter.Optional },
               namespaces: new[] { "PetShop.Controllers" }
           );

            routes.MapRoute(
               name: "ShopMeo",
               url: "danh-sach-san-pham",
               defaults: new { controller = "Shopping", action = "ShopForCat", id = UrlParameter.Optional },
               namespaces: new[] { "PetShop.Controllers" }
           );

            routes.MapRoute(
            name: "ChiTiet",
            url: "chi-tiet/danh-sach-san-pham-{id}",
            defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional },
            namespaces: new[] { "PetShop.Controllers" }
            );

            routes.MapRoute(
          name: "GioHang",
          url: "gio-hang",
          defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
          namespaces: new[] { "PetShop.Controllers" }
          );

                    routes.MapRoute(
           name: "KhuyenMai",
           url: "san-pham-khuyen-mai",
           defaults: new { controller = "Home", action = "PromotionProduct", id = UrlParameter.Optional },
           namespaces: new[] { "PetShop.Controllers" }
           );

            routes.MapRoute(
        name: "DanhMuc",
        url: "san-pham-theo-danh-muc-{id}",
        defaults: new { controller = "Home", action = "ListByMenuSide", id = UrlParameter.Optional },
        namespaces: new[] { "PetShop.Controllers" }
        );

            routes.MapRoute(
     name: "DonHang",
     url: "danh-sach-don-hang-{id}",
     defaults: new { controller = "Customer", action = "ListOrderCustomer", id = UrlParameter.Optional },
     namespaces: new[] { "PetShop.Controllers" }
     );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PetShop.Controllers" }
            );
        }
    }
}
