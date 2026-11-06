using TrainingCenter.Api.Models;
using TrainingCenter.Api.Services;
using Xunit;

namespace TrainingCenter.Tests
{
    public class DiscountServiceTests
    {
        private readonly DiscountService _discountService = new();

        [Fact]
        public void Calculate_GraduateUser_Applies30Percent()
        {
            var courses = new[]
            {
                new Course { 
                    Id = "1", 
                    Title = "Курс номер 1", 
                    Price = 1000m, 
                    StartDate = DateTime.UtcNow.AddDays(30) 
                }
            };

            var user = new UserProfile { IsGraduate = true };

            var result = _discountService.Calculate(courses, user, DateTime.UtcNow);

            Assert.Equal(700m, result.TotalFinalPrice);
            Assert.Equal(0.30m, result.Items[0].Discount);
        }

        [Fact]
        public void Calculate_HotGroup_Applies20Percent()
        {
            var courses = new[]
            {
                new Course { 
                    Id = "2", 
                    Title = "Курс номер 2", 
                    Price = 1000m,
                    StartDate = DateTime.UtcNow.AddDays(3) 
                }
            };

            var user = new UserProfile { IsGraduate = false };

            var result = _discountService.Calculate(courses, user, DateTime.UtcNow);

            Assert.Equal(800m, result.TotalFinalPrice);
            Assert.Equal(0.20m, result.Items[0].Discount);
        }

        [Fact]
        public void Calculate_GraduateAndHotGroup_CheckMaxDiscount()
        {
            var courses = new[]
            {
                new Course { 
                    Id = "3", 
                    Title = "Курс номер 3", 
                    Price = 1000m,
                    StartDate = DateTime.UtcNow.AddDays(2) 
                }
            };

            var user = new UserProfile { IsGraduate = true };

            var result = _discountService.Calculate(courses, user, DateTime.UtcNow);

            //Max = 30%
            Assert.Equal(700m, result.TotalFinalPrice);
            Assert.Equal(0.30m, result.Items[0].Discount);
        }

        [Fact]
        public void Calculate_HotGroup_TotalDiscount()
        {
            var courses = new[]
            {
                //Менее 5 дней, скидка 20%
                new Course {
                    Id = "4",
                    Title = "Курс номер 4",
                    Price = 1000m,
                    StartDate = DateTime.UtcNow.AddDays(2)
                },
                //Скидки нет
                new Course {
                    Id = "5",
                    Title = "Курс номер 5",
                    Price = 1000m,
                    StartDate = DateTime.UtcNow.AddDays(6)
                }
            };

            var user = new UserProfile { IsGraduate = false };

            var result = _discountService.Calculate(courses, user, DateTime.UtcNow);

            Assert.Equal(1800m, result.TotalFinalPrice);
            Assert.Equal(0.20m, result.Items[0].Discount);
            Assert.Equal(0.00m, result.Items[1].Discount);
        }

        [Fact]
        public void Calculate_GraduateUser_TotalDiscount()
        {
            var courses = new[]
            {
                //Менее 5 дней, скидка 20%, но должна примениться максимальная 30%
                new Course {
                    Id = "4",
                    Title = "Курс номер 4",
                    Price = 1000m,
                    StartDate = DateTime.UtcNow.AddDays(2)
                },
                //Скидки нет, но должна быть применена 30%
                new Course {
                    Id = "5",
                    Title = "Курс номер 5",
                    Price = 1000m,
                    StartDate = DateTime.UtcNow.AddDays(60)
                }
            };

            var user = new UserProfile { IsGraduate = true };

            var result = _discountService.Calculate(courses, user, DateTime.UtcNow);

            Assert.Equal(1400m, result.TotalFinalPrice);
            Assert.Equal(0.30m, result.Items[0].Discount);
            Assert.Equal(0.30m, result.Items[1].Discount);
        }
    }
}
