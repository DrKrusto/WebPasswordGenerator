using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebPasswordGenerator.Models;

namespace WebPasswordGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData.Add(new KeyValuePair<string, object>("FirstHalfOfPassword", Request.Cookies["firstHalfOfPassword"]));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GenerateSimplePassword(string firstHalfOfPassword, string softwareName, string specialChar)
        {
            string generatedPassword = firstHalfOfPassword;
            generatedPassword += TransferLettersToNumbers(softwareName.Substring(0, 3));
            if (softwareName.Length >= 4)
                generatedPassword += softwareName[3].ToString().ToUpper();
            generatedPassword += specialChar;
            return new JsonResult(new
            {
                Password = generatedPassword
            });
        }

        [HttpGet]
        public JsonResult GenerateComplexPassword(string softwareName, string firstHalfOfPassword, string specialChar, int maxAmountOfNumbers)
        {
            string generatedPassword = firstHalfOfPassword;
            string numbersLength = "";
            string higher = "";
            if (softwareName.Length > firstHalfOfPassword.Length)
                for (var i = 0; i < firstHalfOfPassword.Length; i++)
                {
                    numbersLength += firstHalfOfPassword[i].ToString() + softwareName[i].ToString();
                }
            else
                for (var i = 0; i < softwareName.Length; i++)
                {
                    numbersLength += softwareName[i].ToString() + firstHalfOfPassword[i].ToString();
                }
            numbersLength = TransferLettersToNumbers(numbersLength);
            if (maxAmountOfNumbers < numbersLength.Length)
                numbersLength = numbersLength.Remove(maxAmountOfNumbers);
            generatedPassword += numbersLength;
            generatedPassword += GetMostUsedLetter(softwareName + firstHalfOfPassword);
            generatedPassword += specialChar;
            return new JsonResult(new
            {
                Password = generatedPassword
            });
        }

        public char GetMostUsedLetter(string word)
        {
            word = word.ToUpper();
            Dictionary<char, int> lettersCount = new Dictionary<char, int>();
            char mostUsedLetter = word[0];
            foreach (char letter in word)
            {
                if (!lettersCount.ContainsKey(letter))
                    lettersCount.Add(letter, 1);
                else
                {
                    lettersCount[letter]++;
                    if (lettersCount[letter] > lettersCount[mostUsedLetter])
                    {
                        lettersCount[mostUsedLetter] = lettersCount[letter];
                    }
                }
            }
            return mostUsedLetter;
        }

        public string TransferLettersToNumbers(string letters)
        {
            string numbers = "";
            foreach (char letter in letters.ToUpper())
            {
                switch (letter)
                {
                    case 'A':
                        numbers += "1";
                        break;
                    case 'B':
                        numbers += "2";
                        break;
                    case 'C':
                        numbers += "3";
                        break;
                    case 'D':
                        numbers += "4";
                        break;
                    case 'E':
                        numbers += "5";
                        break;
                    case 'F':
                        numbers += "6";
                        break;
                    case 'G':
                        numbers += "7";
                        break;
                    case 'H':
                        numbers += "8";
                        break;
                    case 'I':
                        numbers += "9";
                        break;
                    case 'J':
                        numbers += "10";
                        break;
                    case 'K':
                        numbers += "11";
                        break;
                    case 'L':
                        numbers += "12";
                        break;
                    case 'M':
                        numbers += "13";
                        break;
                    case 'N':
                        numbers += "14";
                        break;
                    case 'O':
                        numbers += "15";
                        break;
                    case 'P':
                        numbers += "16";
                        break;
                    case 'Q':
                        numbers += "17";
                        break;
                    case 'R':
                        numbers += "18";
                        break;
                    case 'S':
                        numbers += "19";
                        break;
                    case 'T':
                        numbers += "20";
                        break;
                    case 'U':
                        numbers += "21";
                        break;
                    case 'V':
                        numbers += "22";
                        break;
                    case 'W':
                        numbers += "23";
                        break;
                    case 'X':
                        numbers += "24";
                        break;
                    case 'Y':
                        numbers += "25";
                        break;
                    case 'Z':
                        numbers += "26";
                        break;
                }
            }
            return numbers;
        }

        [HttpPost]
        public void RememberPassword(string cookieValue)
        {
            if (cookieValue == null)
                cookieValue = "";
            CookieOptions cookieOptions = new CookieOptions() { SameSite = SameSiteMode.Strict, Secure = true, Expires = DateTime.Now.AddDays(500) };
            Response.Cookies.Append("firstHalfOfPassword", cookieValue, cookieOptions);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
