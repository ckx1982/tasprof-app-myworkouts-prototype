using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Tasprof.App.MyWorkouts.Fragments;

namespace Tasprof.App.MyWorkouts
{
    [Activity(Label = "", Theme = "@style/Theme.MyTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.contentFrameLayout1, new WorkoutsFragment()).Commit();

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.contentFrameLayout1, new WorkoutsFragment()).Commit();
                    return true;
                case Resource.Id.navigation_dashboard:
                    SupportFragmentManager.BeginTransaction().Replace(Resource.Id.contentFrameLayout1, new DashboardFragment()).Commit();

                    return true;
            }

            return false;
        }
    }
}

