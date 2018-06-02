﻿namespace AspNetCoreMvcTest.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }
}
