using Library.TheraHealth.Models;

namespace Maui.TheraHealth.Views;

public partial class PhysicianView : ContentPage
{
	public PhysicianView()
	{
		InitializeComponent();
		BindingContext = new Physician();
	}
}