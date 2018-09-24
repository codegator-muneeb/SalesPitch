using MarkLight.Views.UI;
using MarkLight.Examples.Data;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using MarkLight;

public class MainMenu : UIView
{
	public ViewSwitcher ContentViewSwitcher;
   
	#region Fields

	public MarkLight.ObservableList<Card> MissionCards;
	private System.Random _random = new System.Random();
    public string MenuShow;
    public string MenuHide;
    public _bool IsMenu;
    #endregion

    #region Methods

    /// <summary>
    /// Initializes the view.
    /// </summary>
    public override void Initialize()
	{
		base.Initialize();
        IsMenu.Value = false;
        MissionCards = new MarkLight.ObservableList<Card>();
        // generate random cards            
        for (int i = 1; i <= 4; ++i)
		{
			Add(i);
		}
	}

	/// <summary>
	/// Adds new card to the list.
	/// </summary>
	public void Add(int i)
	{            
		var card = new Card { CardRank = _random.Next(11, 15), CardSuit = (CardSuit)i };
		MissionCards.Add(card);
	}
	public void MissionClick()
	{
		ContentViewSwitcher.SwitchTo(1);
	}

	public void ExchangesClick(){}
	public void ServicesClick(){}
	public void AwardsClick(){}

	public void Back()
	{
		ContentViewSwitcher.Previous();
	}
	public void Next()
	{
		ContentViewSwitcher.Next();
	}
	public void Home()
	{
        IsMenu.Value = !IsMenu.Value;


    }
	#endregion
}