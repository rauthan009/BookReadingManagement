using BookReadingEvent.Data;
using System;
using System.Linq.Expressions;

namespace BookReadingEvent.Web.Models
{
    /// <summary>
    /// Class contains the view model for comments returning author of the comment and the text
    /// </summary>
    public class CommentViewModel
    {
        public string Text { get; set; }

        public string Author { get; set; }

        /// <summary>
        /// An expression that takes the comment entity as input and returns the commentview model
        /// </summary>
        public static Expression<Func<Comment, CommentViewModel>> ViewModel
        {
            get
            {
                return c => new CommentViewModel()
                {
                    Text = c.Text,
                    Author = c.Author.FullName
                };
            }
        }
    }
}