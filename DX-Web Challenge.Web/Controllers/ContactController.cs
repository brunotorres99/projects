using DX_Web_Challenge.Core;
using DX_Web_Challenge.Core.Criteria;
using DX_Web_Challenge.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DX_Web_Challenge.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly string _apiBaseURL;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
            _apiBaseURL = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ApiBaseURL"];
        }

        public  IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadContacts()
        {
            try
            {

                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
                // Skiping number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();
                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();
                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                // Sort Column Direction ( asc ,desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10,20,50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int pageNumber = (skip / pageSize) + 1;

                var criteria = new ContactCriteria
                {
                    SearchQuery = searchValue,
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    SortField = sortColumn,
                    SortOrder = sortColumnDirection
                };

                var apiBaseURL = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["ApiBaseURL"];
                var builder = new UriBuilder($"{apiBaseURL}/api/Contact");
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["SearchQuery"] = criteria.SearchQuery;
                query["PageSize"] = criteria.PageSize?.ToString();
                query["PageNumber"] = criteria.PageNumber?.ToString();
                query["SortField"] = criteria.SortField;
                query["SortOrder"] = criteria.SortOrder;
                builder.Query = query.ToString();
                string url = builder.ToString();

                using var httpClient = new HttpClient();
                using var response = await httpClient.GetAsync(url);
                string apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<SearchResult<ContactDTO>>(apiResponse);

                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = result.RecordCount, recordsTotal = result.RecordCount, data = result.Records });

            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var builder = new UriBuilder($"{_apiBaseURL}/api/Contact/{id}");
            string url = builder.ToString();

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ContactDTO>(apiResponse);

            return View(new ResponseObject<ContactDTO>{ Value = result });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ResponseObject<ContactDTO> contact)
        {
            await LoadPhotoAsync();

            var builder = new UriBuilder($"{_apiBaseURL}/api/Contact/{id}");
            string url = builder.ToString();

            using var httpClient = new HttpClient();
            var content = JsonConvert.SerializeObject(contact.Value);
            using var httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            using var response = await httpClient.PutAsync(url, httpContent);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseObject<ContactDTO>>(apiResponse);

            return View(result);

            async Task LoadPhotoAsync()
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count == 0) return;

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await file.CopyToAsync(ms);
                        var fileBytes = ms.ToArray();
                        contact.Value.Photo = $"data:{file.ContentType};base64,{ Convert.ToBase64String(fileBytes)}";
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
 
            var builder = new UriBuilder($"{_apiBaseURL}/api/Contact/{id}");
            string url = builder.ToString();

            using var httpClient = new HttpClient();
            using var response = await httpClient.DeleteAsync(url);
            string apiResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseObject<ContactDTO>>(apiResponse);

            return RedirectToAction("Index");
        }
    }
}
