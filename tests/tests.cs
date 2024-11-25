using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace LaheKvass.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private IWebDriver driver;
        private string baseUrl = "http://localhost:44331/"; // Update with your local or hosted application URL

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\Users\opilane\source\repos\LaheKvass\drivers\");

            // Maximize the browser window
            driver.Manage().Window.Maximize();
            // Set an implicit wait time
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }

        [Test]
        public void Register_NewAccount_ShouldSucceed()
        {
            driver.Navigate().GoToUrl(baseUrl + "Account/Register");

            //    // Fill out the registration form
            //    driver.FindElement(By.Id("FirstName")).SendKeys("John");
            //    driver.FindElement(By.Id("LastName")).SendKeys("Doe");
            //    driver.FindElement(By.Id("Gender")).SendKeys("Male");
            //    driver.FindElement(By.Id("Email")).SendKeys("johndoe@example.com");
            //    driver.FindElement(By.Id("Password")).SendKeys("password123");

            //    // Submit the form
            //    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            //    // Assert: Verify redirect or success message
            //    if (!driver.Url.Contains("Account/Login"))
            //    {
            //        throw new Exception("Test failed: User was not redirected to Login page after registration.");
            //    }
        }

        //[Test]
        //public void Login_ValidCredentials_ShouldSucceed()
        //{
        //    driver.Navigate().GoToUrl(baseUrl + "Account/Login");

        //    // Fill out the login form
        //    driver.FindElement(By.Id("Email")).SendKeys("johndoe@example.com");
        //    driver.FindElement(By.Id("Password")).SendKeys("password123");

        //    // Submit the form
        //    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        //    // Assert: Verify successful login (e.g., redirection or welcome message)
        //    if (!driver.Url.Contains("Book/Introduction"))
        //    {
        //        throw new Exception("User was not redirected to Introduction page after login.");
        //    }

        //}

        //[Test]
        //public void Logout_ShouldRedirectToIntroduction()
        //{
        //    driver.Navigate().GoToUrl(baseUrl + "Account/Login");

        //    // Perform login
        //    driver.FindElement(By.Id("Email")).SendKeys("johndoe@example.com");
        //    driver.FindElement(By.Id("Password")).SendKeys("password123");
        //    driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        //    // Perform logout
        //    driver.Navigate().GoToUrl(baseUrl + "Account/Logout");

        //    // Assert: Verify redirect to introduction
        //    if (!driver.Url.Contains("Book/Introduction"))
        //    {
        //        throw new Exception("User was not redirected to Introduction page after logout.");
        //    }
        //}
    }
}