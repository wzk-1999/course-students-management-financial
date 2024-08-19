using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Collections.Generic;
using System.Threading.Channels;
using Zhankui_Wang_Prob_Asst_3_Part_1.Models;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CollegeDbContext _context;

        public CoursesController(CollegeDbContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            ViewData["CurrentFilter"] = searchString;

            ViewData["CurrentSort"] = sortOrder;

            ViewData["CodeSortParm"] = sortOrder == "Code" ? "code_desc" : "Code";
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["SectionSortParm"] = sortOrder == "Section" ? "section_desc" : "Section";
            ViewData["TermSortParm"] = sortOrder == "Term" ? "term_desc" : "Term";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";
            ViewData["ProgramSortParm"] = sortOrder == "Program" ? "program_desc" : "Program";
            //ViewData["IsOpenToEnrollParm"] = sortOrder == "IsOpenToEnroll" ? "IsOpenToEnroll_desc" : "IsOpenToEnroll";

            var courses = from c in _context.Courses.Include(c => c.Program)
                          select c;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "code_desc":
                    courses = courses.OrderByDescending(c => c.Code);
                    break;
                case "Code":
                    courses = courses.OrderBy(c => c.Code);
                    break;
                case "Title":
                    courses = courses.OrderBy(c => c.Title);
                    break;
                case "title_desc":
                    courses = courses.OrderByDescending(c => c.Title);
                    break;
                case "Section":
                    courses = courses.OrderBy(c => c.Section);
                    break;
                case "section_desc":
                    courses = courses.OrderByDescending(c => c.Section);
                    break;
                case "Term":
                    courses = courses.OrderBy(c => c.Term);
                    break;
                case "term_desc":
                    courses = courses.OrderByDescending(c => c.Term);
                    break;
                case "Year":
                    courses = courses.OrderBy(c => c.Year);
                    break;
                case "year_desc":
                    courses = courses.OrderByDescending(c => c.Year);
                    break;
                case "Program":
                    courses = courses.OrderBy(c => c.Program.Name);
                    break;
                case "program_desc":
                    courses = courses.OrderByDescending(c => c.Program.Name);
                    break;
                    //case "IsOpenToEnroll":
                    //    courses = courses.OrderBy(c => c.IsOpenToEnroll);
                    //    break;
                    //case "IsOpenToEnroll_desc":
                    //    courses = courses.OrderByDescending(c => c.IsOpenToEnroll);
                    //    break;

            }

            int pageSize = 10;
            int count = await courses.CountAsync();
            var pageNumberValue = pageNumber ?? 1;

            var items = Service.PageLogic.GetPagedList(await courses.ToListAsync(), pageNumberValue, pageSize);

            ViewData["TotalPages"] = Convert.ToInt32(Math.Ceiling(count / (double)pageSize));
            ViewData["CurrentPage"] = pageNumberValue;

            return View(items);


            //// only change the sort sequence of the dataset, do not need to retrive from db again 
            //return View(await courses.AsNoTracking().ToListAsync());
            ////By default, Entity Framework tracks the changes on the entities it retrieves from the database.
            ////When you use AsNoTracking(), Entity Framework does not keep track of changes to the entities
            ////resulting in better performance and reduced memory usage
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Program)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Name");
            ////add a default but hidden value in the term drop list called "select a term"
            //var terms = _context.Terms.Select(t => new { t.Id, t.Semester }).ToList();
            //terms.Insert(0, new { Id = 0, Semester = "Select a term" });
            ViewData["Term"] = new SelectList(_context.Terms, "Semester", "Semester");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Title,Section,Term,Year,ProgramId")] Course course)
        {
            ModelState.Remove(nameof(course.Program));
            if (ModelState.IsValid)
            {
                course.ValidateYear();      // validate the year input
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Name", course.ProgramId);
            //add a default but hidden value in the term drop list called "select a term"
            //var terms = _context.Terms.Select(t => new { t.Id, t.Semester }).ToList();
            //terms.Insert(0, new { Id = 0, Semester = "Select a term" });
            ViewData["Term"] = new SelectList(_context.Terms, "Semester", "Semester");
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id", course.ProgramId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Title,Section,Term,Year,ProgramId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id", course.ProgramId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Program)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult NewStudent()
        {
            //ViewData["Title"] = "New Student";
            ViewBag.TypeOptions = new SelectList(_context.StudentTypes, "Type", "Type");
            ViewBag.ProgramOptions = new SelectList(_context.Programs, "Id", "Name");
            ViewBag.CityOptions = new SelectList(_context.Cities, "Id", "Name");

            var student = new Student();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> NewStudent(Student student)
        {
            ModelState.Remove(nameof(student.Type));
            ModelState.Remove(nameof(student.City));
            ModelState.Remove(nameof(student.Program));
            ModelState.Remove(nameof(student.StudentID));
            ModelState.Remove(nameof(student.FinancialStatement));

            // Retrieve the "New Student" FeePolicy
            var feePolicy = await _context.FeePolicy
                .FirstOrDefaultAsync(fp => fp.Category == "New Student");

            if (feePolicy == null)
            {
                // Handle the case where the "New Student" FeePolicy is not found
                ModelState.AddModelError("", "Fee policy for new students not found.");
                return View(student);
            }

            // Create the FinancialStatement
            var financialStatement = new FinancialStatement
            {
                LastChanged = DateTime.Now,
                FeePolicy = feePolicy
            };

            // Create the StatementEntry for the Registration Fee
            var statementEntry = new StatementEntry
            {
                DateCreated = DateTime.Now,
                Description = "Registration Fee",
                Value = feePolicy.RegistrationFee,
                FinancialStatement = financialStatement
            };

            // Add the StatementEntry to the FinancialStatement
            financialStatement.StatementEntries.Add(statementEntry);

            // Add FinancialStatement to the context first to ensure it's tracked
            _context.FinancialStatement.Add(financialStatement);

            student.FinancialStatement = financialStatement;

            if (ModelState.IsValid)
            {
               
                //student.Status = StudentStatus.ELIGIBLE; // Set default status
                var city = _context.Cities.Find(student.CityId);
                if (city != null)
                {
                    student.City = city;
                }

                student.PostalCode = Utilities.Utility.PostalCode(student.PostalCode);
                
                // Save student to the database
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction(nameof(Student)); // Redirect to Student view
            }

            ViewBag.TypeOptions = new SelectList(_context.StudentTypes, "Type", "Type");
            ViewBag.ProgramOptions = new SelectList(_context.Programs, "Id", "Name");
            ViewBag.CityOptions = new SelectList(_context.Cities, "Id", "Name");

            return View("NewStudent", student);
        }



        public IActionResult Course(int id)
        {
            var course = _context.Courses
          .Include(c => c.Students)     // Load the Students collection
          .ThenInclude(c => c.City) // Include the City associated with each Student
          .Include(c=>c.Students)
          .ThenInclude(c=>c.FinancialStatement)  //Include and ThenInclude very important
          .ThenInclude(c=>c.StatementEntries)
          .Include(c => c.Students)
          .ThenInclude(c => c.FinancialStatement)
          .ThenInclude(c=>c.FeePolicy)
          .Include(c => c.Program)   // Include the Program for the Course
          .FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseViewModel
            {
                CourseId = id,
                CourseTitle = course.Title,
                IsOpenToEnroll = course.IsOpenToEnroll,
                EnrolledStudents = course.Students.ToList(),
                Section = course.Section,
                Term = course.Term,
                Year = course.Year,
                EligibleStudents = new SelectList(
                _context.Students
                        .Where(s => s.ProgramId == course.ProgramId
                        //Obviously, listed students must have already been
                        //accepted into the program containing the course
                        &&
                       (s.Status == StudentStatus.ELIGIBLE || s.Status == StudentStatus.ENROLLED)
                        //When Student enrolls into a course, the status of the Student changes
                        //from ELIGIBLE to ENROLLED.
                        //A Student is allowed to enroll into a course, only if the status of the
                        //Student is either ELIGIBLE or ENROLLED.
                        &&
                        !course.Students.Contains(s)
                        //this course already has this student
                        )
                        .Select(s => new
                        {
                            s.StudentID,
                            FullName = s.LastName + ", " + s.FirstName
                        }),
                    "StudentID",
                    "FullName")
            };

            return View(viewModel);
        }

        public IActionResult Student()
        {

            var students = _context.Students.Include(s => s.Program).Include(c=>c.Courses).Include(s => s.City).ToList();
            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudent(int courseId, int selectedStudentId)
        {
            var course = await _context.Courses.Include(c => c.Students).FirstOrDefaultAsync(c => c.Id == courseId);
            var student = await _context.Students.Include(c=>c.Courses).Include(s => s.FinancialStatement)
                                      .ThenInclude(fs => fs.FeePolicy)
                                      .FirstOrDefaultAsync(s => s.StudentID == selectedStudentId);

            if (course == null || student == null || !course.IsOpenToEnroll)
            {
                return NotFound();
            }

            // Add the student to the course
            if (!course.Students.Contains(student))
            {
                course.Students.Add(student);
                student.Courses.Add(course);
            }

            // Check the student's current course load
            var courseLoad = student.CourseLoad;

            // Case 1: First course enrollment (Student will be considered part-time)
            if (courseLoad == 1)
            {
                // Determine the FeePolicy based on the student's type
                FeePolicy feePolicy;
                if (student.Type == "DOMESTIC")
                {
                    feePolicy = await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "Domestic Part-Time");
                }
                else if (student.Type == "INTERNATIONAL")
                {
                    feePolicy = await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "International Part-Time");
                }
                else
                {
                    return BadRequest("Invalid student type.");
                }

                if (feePolicy == null)
                {
                    return NotFound("FeePolicy not found.");
                }

               

                var statementEntries = new List<StatementEntry>
            {
                new StatementEntry { Description = "Registration Fee", Value = feePolicy.RegistrationFee, DateCreated = DateTime.Now },
                new StatementEntry { Description = "Tuition Fee", Value = feePolicy.TuitionFee, DateCreated = DateTime.Now },
                new StatementEntry { Description = "Facilities Fee", Value = feePolicy.FacilitiesFee, DateCreated = DateTime.Now },
                new StatementEntry { Description = "Union Fee", Value = feePolicy.UnionFee, DateCreated = DateTime.Now },
                new StatementEntry { Description = "Alumni Fee", Value = feePolicy.AlumniFee, DateCreated = DateTime.Now },
                new StatementEntry { Description = "Library Fee", Value = feePolicy.LibraryFee, DateCreated = DateTime.Now },
                new StatementEntry { Description = "Tax Rate", Value = feePolicy.TaxRate, DateCreated = DateTime.Now }
            };

                // Create a new FinancialStatement
                var financialStatement = new FinancialStatement
                {
                    LastChanged = DateTime.Now,
                    FeePolicy = feePolicy,
                    StatementEntries=statementEntries
                };

                // Assign the FinancialStatement to the student
                student.FinancialStatement = financialStatement;

                await _context.FinancialStatement.AddAsync(financialStatement);
                foreach(StatementEntry entry in statementEntries)
                {
                    await _context.StatementEntry.AddAsync(entry);
                }
                
            }
            // Case 2: Second course enrollment
            else if (courseLoad == 2)
            {
                // Determine the FeePolicy based on the student's type
                FeePolicy feePolicy;
                if (student.Type == "DOMESTIC")
                {
                    feePolicy = await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "Domestic Part-Time");
                }
                else if (student.Type == "INTERNATIONAL")
                {
                    feePolicy = await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "International Part-Time");
                }
                else
                {
                    return BadRequest("Invalid student type.");
                }

                if (feePolicy == null)
                {
                    return NotFound("FeePolicy not found.");
                }

                // Create a new StatementEntry for the tuition fee
                var tuitionFeeEntry = new StatementEntry
                {
                    Description = "Tuition Fee",
                    Value = feePolicy.TuitionFee,
                    DateCreated = DateTime.Now
                };



                // Add the StatementEntry to the existing FinancialStatement
                student.FinancialStatement.StatementEntries.Add(tuitionFeeEntry);

                _context.StatementEntry.Add(tuitionFeeEntry);
            }
            else if (courseLoad == 3)
            {
                // Third course: Student is full-time
                var feePolicy = student.Type == "DOMESTIC"
                    ? await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "Domestic Full-Time")
                    : await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "International Full-Time");

                if (feePolicy == null)
                    return NotFound(); // Handle this scenario as needed

                var statementEntries = new List<StatementEntry>
            {
                new StatementEntry { Description = "Registration Fee", Value = feePolicy.RegistrationFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Facilities Fee", Value = feePolicy.FacilitiesFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Union Fee", Value = feePolicy.UnionFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Alumni Fee", Value = feePolicy.AlumniFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Library Fee", Value = feePolicy.LibraryFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Tuition Fee", Value = feePolicy.TuitionFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Tuition Fee", Value = feePolicy.TuitionFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Tuition Fee", Value = feePolicy.TuitionFee,DateCreated = DateTime.Now },
                new StatementEntry { Description = "Tax Rate", Value = feePolicy.TaxRate,DateCreated = DateTime.Now }
            };

                var financialStatement = new FinancialStatement
                {
                    FeePolicy = feePolicy,
                    LastChanged = DateTime.UtcNow,
                   StatementEntries = statementEntries
                };
                student.FinancialStatement = financialStatement;

                _context.FinancialStatement.Add(financialStatement);
                foreach (StatementEntry entry in statementEntries)
                {
                    _context.StatementEntry.Add(entry);
                }

            }
            else if (student.IsFullTime)
            {
                // Case 4: Student is already full-time
                var feePolicy = student.Type == "DOMESTIC"
                    ? await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "Domestic Full-Time")
                    : await _context.FeePolicy.FirstOrDefaultAsync(fp => fp.Category == "International Full-Time");

                if (feePolicy == null)
                    return NotFound();

                var statementEntry = new StatementEntry
                {
                    Description = "Tuition Fee",
                    Value = feePolicy.TuitionFee,
                    DateCreated= DateTime.Now
                };

                student.FinancialStatement.StatementEntries.Add(statementEntry);
                await _context.StatementEntry.AddAsync(statementEntry);
            }

            course.Students.Add(student);
            student.Status = StudentStatus.ENROLLED;

            await _context.SaveChangesAsync();

            return RedirectToAction("Course", "Courses", new { id = courseId });
        }

        [HttpGet]
        public IActionResult DropStudent(int courseId, int studentId)
        {
            var course = _context.Courses
                .Include(c => c.Students)
                .FirstOrDefault(c => c.Id == courseId);

            var student = _context.Students.FirstOrDefault(s => s.StudentID == studentId);

            if (course == null || student == null)
            {
                return NotFound();
            }

            // Remove the student from the course
            course.Students.Remove(student);
            student.Courses.Remove(course);

            // Optionally change the student's status if necessary
            // Example: Change status back to ELIGIBLE if not enrolled in any other courses
            if (!student.Courses.Any())
            {
                student.Status = StudentStatus.ELIGIBLE;
            }

            _context.SaveChanges();

            // Redirect back to the course page
            return RedirectToAction(nameof(Course), new { id = courseId });
        }

        public async Task<IActionResult> StudentAccount(int studentId)
        {
            // Retrieve the student along with their financial statement and entries
            var student = await _context.Students
                .Include(s => s.FinancialStatement)
                    .ThenInclude(fs => fs.StatementEntries)
                .Include(s => s.FinancialStatement)
                    .ThenInclude(fs => fs.FeePolicy)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null || student.FinancialStatement == null)
            {
                return NotFound();
            }

          //Student student1=new Student(

          //    );

            // Create a view model to pass the data to the view
            var viewModel = new StudentAccountViewModel
            {
                StudentID = student.StudentID,
                FullName = student.FullName,
                FeePolicy = student.FinancialStatement.FeePolicy?.Category,
                Balance = (decimal)student.Balance,
                StatementEntries = (List<StatementEntry>)student.FinancialStatement.StatementEntries,
                LastChanged = student.FinancialStatement.LastChanged
            };

            return View(viewModel);
        }


        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
