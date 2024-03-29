﻿using Compras.Enums;
using Compras.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compras.Datos.Entities;


public class SeedDb
{
    private readonly DataContext _context;
    private readonly IUserHelper _userHelper;
    private readonly IBlobHelper _blobHelper;

    public SeedDb(DataContext context, IUserHelper userHelper, IBlobHelper blobHelper)
    {
        _context = context;
        _userHelper = userHelper;
        _blobHelper = blobHelper;
    }

    public async Task SeedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
        await CheckCategoriesAsync();
        await CheckCountriesAsync();
        await CheckRolesAsync();
        await CheckUserAsync("1010", "Yoho", "Jalez", "Yoho@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "perfil1.jpg", UserType.Admin);
        await CheckUserAsync("2020", "Jalez", "pro", "Jalez@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "perfil1.jpg", UserType.User);
        //await CheckUserAsync("3030", "Pere", "Sila", "pere@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "precila.jfif", UserType.User);
        await CheckUserAsync("4040", "Goku", "Cacaroto", "ElGoku@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Goku-Face-Image1.png", UserType.User);
        await CheckUserAsync("5050", "Gojan", "Pro", "ElGojan@yopmail.com", "322 311 4620", "Calle Luna Calle Sol", "Gojan.jpg", UserType.User);
        await CheckProductsAsync();

    }


    private async Task CheckCategoriesAsync()
    {
        if (!_context.Categories.Any())
        {
            _context.Categories.Add(new Category { Name = "Tecnología" });
            _context.Categories.Add(new Category { Name = "Ropa" });
            _context.Categories.Add(new Category { Name = "Calzado" });
            _context.Categories.Add(new Category { Name = "Belleza" });
            _context.Categories.Add(new Category { Name = "Nutricion" });
            _context.Categories.Add(new Category { Name = "Deportes" });
            _context.Categories.Add(new Category { Name = "Apple" });
            _context.Categories.Add(new Category { Name = "mascotas" });
            _context.Categories.Add(new Category { Name = "Gamer" });

            await _context.SaveChangesAsync();
        }
    }


    private async  Task CheckProductsAsync()
    {
        if (!_context.Products.Any())
        {
            await AddProductAsync("Adidas Barracuda", 270000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "adidas_barracuda.jpg" });
            await AddProductAsync("Adidas Superstar", 250000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "Adidas_superstar.png" });
            await AddProductAsync("AirPods", 1300000M, 12F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "airpos.png"});
            await AddProductAsync("Audifonos Bose", 870000M, 12F, new List<string>() { "Tecnología" }, new List<string>() { "audifonos_bose.png" });
            await AddProductAsync("Bicicleta Ribble", 12000000M, 6F, new List<string>() { "Deportes" }, new List<string>() { "bicicleta_ribble.png" });
            await AddProductAsync("Camisa Cuadros", 56000M, 24F, new List<string>() { "Ropa" }, new List<string>() { "camisa_cuadros.png" });
            await AddProductAsync("Casco Bicicleta", 820000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "casco_bicicleta.png", "casco.png" });
            await AddProductAsync("iPad", 2300000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "ipad.png" });
            await AddProductAsync("iPhone ", 5200000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "Iphone1.jpg", "iphone2.png"});
            //await AddProductAsync("Mac Book Pro", 12100000M, 6F, new List<string>() { "Tecnología", "Apple" }, new List<string>() { "mac_book_pro.png" });
            //await AddProductAsync("Mancuernas", 370000M, 12F, new List<string>() { "Deportes" }, new List<string>() { "mancuernas.png" });
            //await AddProductAsync("Mascarilla Cara", 26000M, 100F, new List<string>() { "Belleza" }, new List<string>() { "mascarilla_cara.png" });
            //await AddProductAsync("New Balance 530", 180000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance530.png" });
            //await AddProductAsync("New Balance 565", 179000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "newbalance565.png" });
            //await AddProductAsync("Nike Air", 233000M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_air.png" });
            //await AddProductAsync("Nike Zoom", 249900M, 12F, new List<string>() { "Calzado", "Deportes" }, new List<string>() { "nike_zoom.png" });
            //await AddProductAsync("Buso Adidas Mujer", 134000M, 12F, new List<string>() { "Ropa", "Deportes" }, new List<string>() { "buso_adidas.png" });
            //await AddProductAsync("Suplemento Boots Original", 15600M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "Boost_Original.png" });
            //await AddProductAsync("Whey Protein", 252000M, 12F, new List<string>() { "Nutrición" }, new List<string>() { "whey_protein.png" });
            //await AddProductAsync("Arnes Mascota", 25000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "arnes_mascota.png" });
            //await AddProductAsync("Cama Mascota", 99000M, 12F, new List<string>() { "Mascotas" }, new List<string>() { "cama_mascota.png" });
            //await AddProductAsync("Teclado Gamer", 67000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "teclado_gamer.png" });
            await AddProductAsync("PC HD", 980000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "PC2.jpeg" });
            await AddProductAsync("PC Gamer", 132000M, 12F, new List<string>() { "Gamer", "Tecnología" }, new List<string>() { "PC1.jpg" });
            await _context.SaveChangesAsync();
        }
    }
    private async Task AddProductAsync(string name, decimal price, float stock, List<string> categories, List<string> images)
    {
        Product prodcut = new()
        {
            Description = name,
            Name = name,
            Price = price,
            Stock = stock,
            ProductCategories = new List<ProductCategory>(),
            ProductImages = new List<ProductImage>()
        };

        foreach (string? category in categories)
        {
            prodcut.ProductCategories.Add(new ProductCategory { Category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category) });
        }


        foreach (string? image in images)
        {
            Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\products\\{image}", "products", image);
            prodcut.ProductImages.Add(new ProductImage { ImageId = imageId, ImageName = image });
        }

        _context.Products.Add(prodcut);
    }


    private async Task<User> CheckUserAsync(
     string document,
     string firstName,
     string lastName,
     string email,
     string phone,
     string address,
     string image,
     UserType userType)
    {
        User user = await _userHelper.GetUserAsync(email);
        if (user == null)
        {
            Guid imageId = await _blobHelper.UploadBlobAsync($"{Environment.CurrentDirectory}\\wwwroot\\images\\users\\{image}", "Users", image);


            user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                Address = address,
                Document = document,
                City = _context.Cities.FirstOrDefault(),
                UserType = userType,
                ImageId = imageId,
                ImageName = image,

            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());

            string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);

        }

        return user;
    }


    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
        await _userHelper.CheckRoleAsync(UserType.User.ToString());
                                          

    }

    private async Task CheckCountriesAsync()
    {
        if (!_context.countries.Any())
        {
            _context.countries.Add(new Country
            {
                Name = "Colombia",
                States = new List<State>()
            {
                new State()
                {
                    Name = "Antioquia",
                    Cities = new List<City>() {
                        new City() { Name = "Medellín" },
                        new City() { Name = "Itagüí" },
                        new City() { Name = "Envigado" },
                        new City() { Name = "Bello" },
                        new City() { Name = "Sabaneta" },
                        new City() { Name = "La Ceja" },
                        new City() { Name = "La Union" },
                        new City() { Name = "La Estrella" },
                        new City() { Name = "Copacabana" },
                    }
                },
                new State()
                {
                    Name = "Bogotá",
                    Cities = new List<City>() {
                        new City() { Name = "Usaquen" },
                        new City() { Name = "Champinero" },
                        new City() { Name = "Santa fe" },
                        new City() { Name = "Usme" },
                        new City() { Name = "Bosa" },
                    }
                },
                new State()
                {
                    Name = "Valle",
                    Cities = new List<City>() {
                        new City() { Name = "Calí" },
                        new City() { Name = "Jumbo" },
                        new City() { Name = "Jamundí" },
                        new City() { Name = "Chipichape" },
                        new City() { Name = "Buenaventura" },
                        new City() { Name = "Cartago" },
                        new City() { Name = "Buga" },
                        new City() { Name = "Palmira" },
                    }
                },
                new State()
                {
                    Name = "Santander",
                    Cities = new List<City>() {
                        new City() { Name = "Bucaramanga" },
                        new City() { Name = "Málaga" },
                        new City() { Name = "Barrancabermeja" },
                        new City() { Name = "Rionegro" },
                        new City() { Name = "Barichara" },
                        new City() { Name = "Zapatoca" },
                    }
                },
            }
            });
            _context.countries.Add(new Country
            {
                Name = "Estados Unidos",
                States = new List<State>()
            {
                new State()
                {
                    Name = "Florida",
                    Cities = new List<City>() {
                        new City() { Name = "Orlando" },
                        new City() { Name = "Miami" },
                        new City() { Name = "Tampa" },
                        new City() { Name = "Fort Lauderdale" },
                        new City() { Name = "Key West" },
                    }
                },
                new State()
                {
                    Name = "Texas",
                    Cities = new List<City>() {
                        new City() { Name = "Houston" },
                        new City() { Name = "San Antonio" },
                        new City() { Name = "Dallas" },
                        new City() { Name = "Austin" },
                        new City() { Name = "El Paso" },
                    }
                },
                new State()
                {
                    Name = "California",
                    Cities = new List<City>() {
                        new City() { Name = "Los Angeles" },
                        new City() { Name = "San Francisco" },
                        new City() { Name = "San Diego" },
                        new City() { Name = "San Bruno" },
                        new City() { Name = "Sacramento" },
                        new City() { Name = "Fresno" },
                    }
                },
            }
            });
            _context.countries.Add(new Country
            {
                Name = "Ecuador",
                States = new List<State>()
            {
                new State()
                {
                    Name = "Pichincha",
                    Cities = new List<City>() {
                        new City() { Name = "Quito" },
                    }
                },
                new State()
                {
                    Name = "Esmeraldas",
                    Cities = new List<City>() {
                        new City() { Name = "Esmeraldas" },
                    }
                },
            }
            });
        }

        await _context.SaveChangesAsync();
    }


}
