using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using TodoApp;

namespace TodoDroid
{
    [Activity(Label = "ReviewActivity")]
    public class ReviewActivity : ListActivity
    {
        private List<string> Todos = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var db = GetConnection();

            foreach (var todo in db.Table<TodoItem>())
            {
                Todos.Add(todo.ToString());
            }
            
            ListAdapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1,Todos);
        }

        private SQLite.SQLiteConnection GetConnection()
        {
            var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbPath = System.IO.Path.Combine(folder, "todo.db3");
            var conn = new SQLite.SQLiteConnection(dbPath);
            conn.CreateTable<TodoItem>();
            return conn;
        }
    }
}