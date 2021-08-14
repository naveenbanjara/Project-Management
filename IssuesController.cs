using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management
{
    public class IssuesController : Controller
    {
        private readonly ProjectContext _context;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".jpg",".jpeg",".png" };
        private readonly string _targetFilePath;

        public IssuesController(ProjectContext context, IConfiguration config, IWebHostEnvironment env)
        {
            _context = context;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            //_targetFilePath = config.GetValue<string>("StoredFilesPath");
            _targetFilePath = Path.Combine(env.ContentRootPath, "Content/Attachments");
        }

        // GET: Issues
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Issues.Include(i => i.Project);
            return View(await projectContext.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .Include(i => i.FileUploads)                
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            IEnumerable<FileUpload> files = issue.FileUploads.Where(f => f.IssueID == issue.ID);

            return PartialView("_DetailsIssueModelPartial",issue);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {            
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "ID");
            //Issue issue = new Issue();
            //return PartialView("_IssueModelPartial",issue);            
            return View();
        }

        // POST: Issues/Create [Bind("Title,Description,Status,StartDate,EndDate,ProjectID")] ,List<IFormFile>? formFiles)
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Status,StartDate,EndDate,ProjectID,FormFiles")] Issue issue)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(issue);
                    await _context.SaveChangesAsync();                    
                    List<string> filenames = new List<string>();
                    if (issue.FormFiles != null)
                    {                       
                        foreach (var formFile in issue.FormFiles)
                        {
                            var formFileContent =
                                await FileHelpers
                                    .ProcessFormFile<FileUploader>(
                                        formFile, ModelState, _permittedExtensions,
                                        _fileSizeLimit);

                            if (!ModelState.IsValid)
                            {
                                ViewData["ErrorMessage"] = "Please correct the form.";

                                return View();
                            }
                            
                            var trustedFileNameForFileStorage = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(formFile.FileName);
                            var filePath = Path.Combine(
                                _targetFilePath, trustedFileNameForFileStorage);
                            

                            using (var fileStream = System.IO.File.Create(filePath))
                            {
                                await fileStream.WriteAsync(formFileContent);
                                
                                FileUpload fileupload = new FileUpload();
                                fileupload.FileName = trustedFileNameForFileStorage;
                                filenames.Add(trustedFileNameForFileStorage);
                                fileupload.IssueID = issue.ID;
                                _context.Add(fileupload);
                                // To work directly with the FormFiles, use the following
                                // instead:
                                //await formFile.CopyToAsync(fileStream);
                            }
                        }                        
                    }
                   
                    await _context.SaveChangesAsync();
                 
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "ID", issue.ProjectID);
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "ID", issue.ProjectID);
            return PartialView("_EditIssueModelPartial", issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditIssue(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueToUpdate = await _context.Issues.FirstOrDefaultAsync(p => p.ID == id);

            if (await TryUpdateModelAsync<Issue>(
                issueToUpdate,
                "",
                i => i.Title, i => i.Description, i => i.Status, i => i.StartDate, i => i.EndDate, i => i.ProjectID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewData["ProjectID"] = new SelectList(_context.Projects, "ID", "ID", issueToUpdate.ProjectID);
            return View(issueToUpdate);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issues
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (issue == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return PartialView("_DeleteIssueModelPartial",issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Issues.Remove(issue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
            
        }       

        private bool IssueExists(int id)
        {
            return _context.Issues.Any(e => e.ID == id);
        }
    }

    public class FileUploader
    {
        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }

        [Display(Name = "Note")]
        [StringLength(50, MinimumLength = 0)]
        public string Note { get; set; }
    }

}
