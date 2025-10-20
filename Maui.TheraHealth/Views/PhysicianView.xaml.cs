using Library.TheraHealth.Models;
using Library.TheraHealth.Services;

namespace Maui.TheraHealth.Views;

public partial class PhysicianView : ContentPage
{
	public int physicianId { get; set; }
	public PhysicianView()
	{
		InitializeComponent();
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		if (physicianId == 0)
		{
			BindingContext = new Physician();
		}
		else
		{
			BindingContext = new Physician(physicianId);
		}
	}

	private void OkClicked(object sender, EventArgs e)
	{
		PhysicianServiceProxy.Current.AddOrUpdate(BindingContext as Physician);

		Shell.Current.GoToAsync("//MainPage");
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}