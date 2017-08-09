using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using System.IO;
using TodoApp;

namespace TodoDroid
{
    [Activity(Label = "TodoDroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView FechaDisplay;
        private Button FechaButton;
        private DateTime fecha;
        private const int DatePickerId = 0;

        private List<string> tareas = new List<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            SetConnection();

            FechaDisplay = FindViewById<TextView>(Resource.Id.FechaFin);
            FechaButton = FindViewById<Button>(Resource.Id.FechaButton);
            FechaButton.Click += OnFechaButtonClicked;

            var saveButton = FindViewById<Button>(Resource.Id.GuardarButton);
            var cancelButton = FindViewById<Button>(Resource.Id.CancelarButton);
            var reviewButton = FindViewById<Button>(Resource.Id.RevisarButton);

            var todo = FindViewById<EditText>(Resource.Id.TodoEntry);
            var prioridad = FindViewById<EditText>(Resource.Id.PrioridadEntry);

            saveButton.Click += (s, e) =>
            {
                tareas.Add($"{todo.Text} Prioridad: {prioridad.Text} - {fecha}");
                todo.Text = string.Empty;
                prioridad.Text = string.Empty;


            };

            cancelButton.Click += (s, e) =>
            {
                todo.Text = string.Empty;
                prioridad.Text = string.Empty;
            };

            reviewButton.Click += (s, e) =>
            {
                var intent = new Intent(this, typeof(ReviewActivity));
                intent.PutStringArrayListExtra("tareas", tareas);
                StartActivity(intent);
            };

            fecha = DateTime.Now;

            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            FechaDisplay.Text = fecha.ToString("d");
        }
        private void OnFechaButtonClicked(object sender, EventArgs e)
        {
            ShowDialog(DatePickerId);
        }

        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this.fecha = e.Date;
            UpdateDisplay();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case DatePickerId:
                    return new DatePickerDialog(this,OnDateSet,fecha.Year,fecha.Month-1,fecha.Day);
                    break;
            }
            return null;
        }
        private SQLite.SQLiteConnection database;

        private void SetConnection()
        {
            var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbPath = Path.Combine(folder, "todo.db3");
            var conn = new SQLite.SQLiteConnection(dbPath);
            conn.CreateTable<TodoItem>();
            database = conn;
        }
    }
}

