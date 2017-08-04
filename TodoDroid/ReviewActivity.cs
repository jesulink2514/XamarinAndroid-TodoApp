using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

namespace TodoDroid
{
    [Activity(Label = "ReviewActivity")]
    public class ReviewActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var tareas = Intent.Extras.GetStringArrayList("tareas") ?? new List<string>();

            ListAdapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,tareas);


        }
    }
}