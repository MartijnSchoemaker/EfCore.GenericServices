﻿using GenericServices.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPageApp.Helpers;
using ServiceLayer.HomeController;
using ServiceLayer.HomeController.Dtos;

namespace RazorPageApp.Pages.Home
{
    public class AddReviewModel : PageModel
    {
        private readonly IAddReviewService _service;

        public AddReviewModel(IAddReviewService service)
        {
            _service = service;
        }

        [BindProperty]
        public AddReviewDto Data { get; set; }

        public void OnGet(int id)
        {
            Data = _service.GetOriginal(id);
            if (!_service.IsValid)
            {
                _service.CopyErrorsToModelState(ModelState, Data);
            }
        }

        //There are two ways to get data. This takes the id as a parameter and picks up the other information from the [BindProperty]
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _service.AddReviewToBook(Data);
            if (_service.IsValid)
                return RedirectToPage("BookUpdated", new { message = "Successfully added a review to the book." });

            //Error state
            _service.CopyErrorsToModelState(ModelState, Data);
            return Page();
        }
    }
}