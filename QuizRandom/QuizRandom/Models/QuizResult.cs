using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;


namespace QuizRandom.Models
{
    public class QuizResult// : MyBindableObject
    {
        public int CorrectCount { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;

        //public void RegisterCorrect()
        //{
        //    CorrectCount += 1;
        //    OnPropertyChanged(nameof(CorrectCount));
        //}

        //public bool IsBetterThan(QuizResult other)
        //{
        //    return CorrectCount > other.CorrectCount;
        //}
    }
}
