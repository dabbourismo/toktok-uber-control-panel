using ElCaptain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ElCaptain
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());

            //UnityConfig.RegisterComponents();

            AutoMapperWebProfile.Run();
        }
    }

    internal class AutoMapperWebProfile : AutoMapper.Profile
    {
        public static void Run() => AutoMapper.Mapper.Initialize(a => { a.AddProfile<AutoMapperWebProfile>(); });

        public AutoMapperWebProfile()
        {
            AllowNullDestinationValues = true;


            CreateMap<VehicleOwner, VehicleOwnerDto>();
            CreateMap<VehicleOwnerDto, VehicleOwner>();

            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Store, StoreDto>();
            CreateMap<StoreDto, Store>();

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();

            CreateMap<DeliveryMan, DeliveryManDto>()
                                .ForMember(dto => dto.VehicleOwnerName, opt => opt.MapFrom(obj => obj.VehicleOwner.Name));
            CreateMap<DeliveryManDto, DeliveryMan>();

            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.ClientName, opt => opt.MapFrom(obj => obj.Client.Name))
                .ForMember(dto => dto.ClientPhone, opt => opt.MapFrom(obj => obj.Client.Phone1))

                .ForMember(dto => dto.StoreName, opt => opt.MapFrom(obj => obj.Store.Name))
                .ForMember(dto => dto.StorePhone, opt => opt.MapFrom(obj => obj.Store.Phone1))
                .ForMember(dto => dto.VehicleNumber, opt => opt.MapFrom(obj => obj.DeliveryMan.VehicleNumber))

                .ForMember(dto => dto.DeliveryManName, opt => opt.MapFrom(obj => obj.DeliveryMan.Username))
                .ForMember(dto => dto.DeliveryManPhone, opt => opt.MapFrom(obj => obj.DeliveryMan.Phone1));
            CreateMap<OrderDto, Order>();

            CreateMap<Payment, PaymentDto>()
             .ForMember(dto => dto.OrderDate, opt => opt.MapFrom(obj => obj.Order.Date))
             .ForMember(dto => dto.ClientName, opt => opt.MapFrom(obj => obj.Order.Client.Name));
            CreateMap<PaymentDto, Payment>();

            CreateMap<Menu, MenuDto>()
           .ForMember(dto => dto.StoreName, opt => opt.MapFrom(obj => obj.Store.Name));
            CreateMap<MenuDto, Menu>();


        }
    }
}
