using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddApplicationError(this HttpResponse response, string message) {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Controller-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Controller-Allow-Origin", "*");
        }

        public static int CaculateAge(this DateTime dateTime) {
            var age = DateTime.Today.Year - dateTime.Year;
            if (dateTime.AddYears(age) > DateTime.Today) {
                age--;
            }
            return age;
        }
    }
}