using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace EMM.Helpers
{
    public class ShowPasswordTriggerAction : TriggerAction<Grid>
    {
        public string IconImageName { get; set; }

        public string EntryPasswordName { get; set; }

        public string EntryTextName { get; set; }

        protected override void Invoke(Grid sender)
        {
            // get the runtime references 
            // for our Elements from our custom control
            var entryPasswordView = ((Grid)((Grid)sender.Parent).Parent).FindByName<Entry>(EntryPasswordName);
            var entryTextView = ((Grid)((Grid)sender.Parent).Parent).FindByName<Entry>(EntryTextName);

            // Switch visibility of Password 
            // Entry field and Text Entry fields
            entryPasswordView.IsVisible =
                           !entryPasswordView.IsVisible;
            entryTextView.IsVisible =
                           !entryTextView.IsVisible;

            // update the Show/Hide button Icon states 
            if (entryPasswordView.IsVisible)
            {
                // Password is not Visible state
                // Setting up Entry curser focus
                entryPasswordView.Focus();
                entryPasswordView.Text = entryTextView.Text;
            }
            else
            {
                // Password is Visible state
                // Setting up Entry curser focus
                entryTextView.Focus();
                entryTextView.Text = entryPasswordView.Text;
            }
        }
    }
}
