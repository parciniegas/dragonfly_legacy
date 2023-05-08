using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Dragonfly.Application.Contracts.Course;
using Dragonfly.Domain.Contracts.Instructor;
using Dragonfly.Domain.Model;
using Dragonfly.UI.Mvc.Models;

namespace Dragonfly.UI.Mvc.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseAppService _courseAppService;
        private readonly IInstructorDomain _instructorDomain;
        private readonly IMapper _mapper;

        public CoursesController(ICourseAppService courseAppService, IInstructorDomain instructorDomain, IMapper mapper)
        {
            _courseAppService = courseAppService;
            _instructorDomain = instructorDomain;
            _mapper = mapper;
        }

        // GET: Course
        [HttpGet]
        public ActionResult Index(string mainParam)
        {
            ViewBag.BreadCrum = "Courses";

            return View();
        }

        [HttpGet]
        public ActionResult IndexGrid(string param)
        {
            var courses = _courseAppService.GetAll();
            var viewCourses = new List<ViewCourse>();
            _mapper.Map(courses, viewCourses);

            return PartialView("_IndexGrid", viewCourses);
        }

        public ActionResult Create()
        {
            ViewBag.BreadCrum = "Courses / Create";
            ViewBag.InstructorId = new SelectList(_instructorDomain.GetAll(), "InstructorId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
                return View(course);

            _courseAppService.Add(course);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}