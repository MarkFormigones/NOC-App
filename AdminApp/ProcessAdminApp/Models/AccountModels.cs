using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
  

namespace Hydron.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
     
    }

    public class RegisterExternalLoginModel
    {
       

        public string UserId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "User Email")]
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
         [Required]
        [Display(Name = "User name")]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Remember me?")]
        public bool remember { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "User Email")]
        public string UserEmail { get; set; }


        [Display(Name = "User mobile")]
        public string UserMobile { get; set; }

        [Display(Name = "User telephone")]
        public string UserTelephone { get; set; }

        [Display(Name = "User First Name")]
        public string UserFName{ get; set; }
        [Display(Name = "User Second Name")]
        public string UserSName { get; set; }
        [Display(Name = "User Last Name")]
        public string UserLName { get; set; }
        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }
        [Display(Name = "User Picture")]
        public string UserPic { get; set; }

        [Display(Name = "User Address")]
        public string UserAddess { get; set; }
        [Display(Name = "User City")]
        public string UserCity { get; set; }
        [Display(Name = "User Country")]
        public string UserCountry { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
         [Required]
           [Display(Name = "Company")]
        public int UserCompanyId { get; set; }
         public virtual IEnumerable<System.Web.Mvc.SelectListItem> CompanyListData { get; set; }
         public virtual IEnumerable<System.Web.Mvc.SelectListItem> CountryListData { get; set; }

    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }


    public class PersonalInfoModel
    {

        public int AdminId { get; set; }
        public int UserId { get; set; }

        [Required]
         [DataType(DataType.EmailAddress)]
        [Display(Name = "User Email")]
        public string UserEmail { get; set; }

         [DataType(DataType.PhoneNumber)]
        [Display(Name = "User mobile")]
        public string UserMobile { get; set; }
         [DataType(DataType.PhoneNumber)]
        [Display(Name = "User telephone")]
        public string UserTelephone { get; set; }
         [Required]
        [Display(Name = "User First Name")]
        public string UserFName { get; set; }
        [Display(Name = "User Second Name")]
        public string UserSName { get; set; }
         [Required]
        [Display(Name = "User Last Name")]
        public string UserLName { get; set; }
        [Display(Name = "User Full Name")]
        public string UserFullName { get; set; }
        [Display(Name = "User Picture")]
        public string UserPic { get; set; }

        [Display(Name = "User Address")]
        public string UserAddess { get; set; }
        [Display(Name = "User City")]
        public string UserCity { get; set; }
         [Required]
        [Display(Name = "User Country")]
        public string UserCountryId { get; set; }

         [Display(Name = "Date of Birth")]
         public DateTime? UserDOB { get; set; }


        public virtual IEnumerable<System.Web.Mvc.SelectListItem> CountryListData { get; set; }



        public string UserAbout { get; set; }
    }
    public class UserProfilOverview
    { 
    }
}
