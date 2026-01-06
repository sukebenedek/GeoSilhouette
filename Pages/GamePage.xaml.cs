using GeoSilhouette.ViewModels;
using CommunityToolkit.Maui.Core.Platform;
using Microsoft.Maui;
namespace GeoSilhouette.Pages;

public partial class GamePage : ContentPage
{
	public GamePage(ViewModels.GameViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        vm.UI_AddGuessToScreen = AddGuessToUI;
        vm.UI_ClearGuesses = ClearGuessesFromUI;
        vm.UI_AddPlaceholderToUI = AddPlaceholderToUI;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Check if the BindingContext is actually your ViewModel
        if (BindingContext is GameViewModel vm)
        {
            vm.OnPageAppearing();

            var originalColor = this.BackgroundColor;

            vm.UI_SetSuccessBackground = (isSuccess) =>
            {
                // If success, set Green. Otherwise, revert to original.
                this.BackgroundColor = isSuccess ? Colors.Green : originalColor;
            };
        }

    }

    public async void ClearGuessesFromUI()
    {
        await GuessEntry.HideKeyboardAsync(CancellationToken.None);

        // This removes every label/stack you added to the container
        DirectionsContainer.Children.Clear();
    }

    public async void AddGuessToUI(string countryName, string directionHint, int distance, bool isEasy)
    {
        await GuessEntry.HideKeyboardAsync(CancellationToken.None);
        for (int i = DirectionsContainer.Children.Count - 1; i >= 0; i--)
        {
            // 1. Get the child as a VisualElement (so we can check HeightRequest)
            if (DirectionsContainer.Children[i] is VisualElement child)
            {
                // 2. Check your condition
                // Note: Comparing doubles exactly can sometimes be tricky, 
                // but for set values like 49, it usually works fine.
                if (child.HeightRequest == 49)
                {
                    DirectionsContainer.Children.RemoveAt(i);
                }
            }
        }

        // 1. Determine Color based on Distance
        Color distanceColor;
        if (distance <= 1200) distanceColor = Colors.Green;
        else if (distance <= 3000) distanceColor = Colors.Orange;
        else distanceColor = Colors.Red;

        // 2. Determine the Text based on Difficulty
        // If Easy: "North • 2000 km"
        // If Not Easy: "North"
        string labelText = isEasy
            ? $"{directionHint} • {distance} km"
            : $"{directionHint}";

        // 3. Create the container
        var guessStack = new VerticalStackLayout
        {
            HeightRequest = 50,
            Spacing = 0
        };

        // 4. Create the Country Name Label
        var nameLabel = new Label
        {
            Text = countryName,
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = 18
        };

        // 5. Create the Hint Label (using the logic from Step 2)
        var hintLabel = new Label
        {
            Text = labelText,
            Margin = new Thickness(16, 0, 0, 0),
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = 18,
            TextColor = distanceColor
        };

        // 6. Assemble and Add
        guessStack.Children.Add(nameLabel);
        guessStack.Children.Add(hintLabel);

        DirectionsContainer.Children.Insert(0, guessStack);
    }
    public void AddPlaceholderToUI()
    {
        // 1. Create the container (Height 49 matches your remove logic)
        var placeholderStack = new VerticalStackLayout
        {
            HeightRequest = 49,
            Spacing = 0
        };

        // 2. Create the Top Label ("A country will appear here")
        var topLabel = new Label
        {
            Text = "A country will appear here",
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = 18
        };

        // 3. Create the Bottom Label ("And a leading direction here")
        var bottomLabel = new Label
        {
            Text = "And a leading direction here",
            // Adjusted bottom margin to 0 so it fits in the 49px height
            Margin = new Thickness(16, 0, 0, 0),
            VerticalTextAlignment = TextAlignment.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = 18,
            TextColor = Colors.Gray
        };

        // 4. Assemble
        placeholderStack.Children.Add(topLabel);
        placeholderStack.Children.Add(bottomLabel);

        // 5. Add to the container (at the top)
        DirectionsContainer.Children.Insert(0, placeholderStack);
    }

}