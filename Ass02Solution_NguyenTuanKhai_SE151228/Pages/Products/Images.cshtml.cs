using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Products
{
    public class ImagesModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;
        private IHostingEnvironment _environment;
        public List<String> Files { get; set; }

        public ImagesModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [Required(ErrorMessage = "Please choose at least one file!")]
        [DataType(DataType.Upload)]
        [Display(Name = "Choose file(s) to upload")]
        //[FileExtensions(Extensions="")]
        [BindProperty]
        public IFormFile[] file { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2"))) return RedirectToPage("/Login");

            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(_environment.WebRootPath, "Images"));
            //Copy File names to Model collection.
            this.Files = new List<String>();
            foreach (string filePath in filePaths)
            {
                this.Files.Add(Path.GetFileName(filePath));
            }
            ViewData["Image"] = new SelectList(Files);
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (file != null)
            {
                foreach (var FileUpload in file)
                {
                    if (FileUpload.FileName.Contains("png") || FileUpload.FileName.Contains(".jpg") || FileUpload.FileName.Contains(".jpeg") || FileUpload.FileName.Contains(".gif"))
                    {
                        var f = Path.Combine(_environment.WebRootPath, "Images", FileUpload.FileName);
                        using (var fileStream = new FileStream(f, FileMode.Create))
                        {
                            await FileUpload.CopyToAsync(fileStream);
                        }
                    }
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
