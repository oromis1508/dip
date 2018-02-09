
using System;
using demo.framework;
using demo.framework.Elements;
using demo.framework.forms;
using NUnit.Core;
using NUnit.Framework;
using OpenQA.Selenium;

public class TutSearchForm : BaseForm {

	private TextBox txbSearchBar = new TextBox(By.Id("search_from_str"),"Search bar");
	private Link lbLogo = new Link(By.XPath("//img[@id='pageLogo']"),"tut.by logo");
	private Button btnSubmitSearch = new Button (By.XPath("//input[@name='search']"),"Search");
	private Link lblTesting = new Link(By.XPath("//a[contains(@href,'a1qa')]"),"A1QA");


    public TutSearchForm()
        : base(By.Id("search_from_str"), "Tut by")
    {
    }

    public void AssertLogo(){
        Assert.AreEqual(lbLogo.IsPresent(), true);
	}
	
    public void SearchFor(String text) {
    	txbSearchBar.SetText(text);
    	btnSubmitSearch.Click();
		Browser.WaitForPageToLoad();

    }
	
	public void AssertA1QaString(){
        Assert.AreEqual(lblTesting.IsPresent(), true);
	}
}
