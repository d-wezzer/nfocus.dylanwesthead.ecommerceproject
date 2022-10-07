<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a name="readme-top"></a>
<!--
*** Thanks for checking out the Best-README-Template. If you have a suggestion
*** that would make this better, please fork the repo and create a pull request
*** or simply open an issue with the tag "enhancement".
*** Don't forget to give the project a star!
*** Thanks again! Now go create something AMAZING! :D
-->



<!-- PROJECT SHIELDS -->
<!--
*** I'm using markdown "reference style" links for readability.
*** Reference links are enclosed in brackets [ ] instead of parentheses ( ).
*** See the bottom of this document for the declaration of the reference variables
*** for contributors-url, forks-url, etc. This is an optional, concise syntax you may use.
*** https://www.markdownguide.org/basic-syntax/#reference-style-links
-->
<!--
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
-->
[![LinkedIn][linkedin-shield]][linkedin-url]



<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject">
    <img src="RepoImages/edgewords_logo.png" alt="Logo" width="246" height="60">
  </a>

<h3 align="center">Edgewords eCommerce Automated User Tests</h3>

  <p align="center">
    Automated end-to-end user tests written in Cucumber and C# to test the Edgewords eCommerce demo site.
    <br />
    <a href="https://www.edgewordstraining.co.uk/"><strong>Edgewords Training »</strong></a>
     
    <a href="https://www.nfocus.co.uk/"><strong>nFocus Testing »</strong></a>
    <br />
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
    </li>
    <li><a href="#license">License</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

###Project summary

The Edgewords eCommerce Automated Test project is a deliverable of undergoing and completing the Edgewords Software Testing course. Thus, this project is my first of many testing applications.

Written almost entirely in C#, the project tackles automation of two end-to-end user requirements for the Edgewords eCommerce demo website. The main objective was to learn about the different test automation tools and put my knowledge into practice.

The project uses the .Net Core Framework to build and run tests. Specflow and WebDriver are the main automation tools for the project, whilst Cucumber is also used to apply a BDD testing style suitable for business facing representatives. SpecFlow's LivingDoc is used to capture and produce pretty html report files.

The two test cases where as follows:
* 1) The test will login to an e-commerce site as a registered user, purchase an item of clothing, apply a discount code and check that the total is correct after the discount & shipping is applied. 
* 2) The test will login to an e-commerce site as a registered user, purchase an item of clothing and go through checkout. It will capture the order number and check the order is also present in the ‘My Orders’ section of the site.

GitHub Actions was used to enable a continuous integration pipeline whenever a push is made to the repository. Lastly, a .batch file was created to further enhance automation of the entire project: run and build the tests, compile test results, generate report, launch report. 

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* [![Dotnet][Dotnet.microsoft.com]][Dotnet-url]  * [![Cucumber][Cucumber.io]][Cucumber-url]
* [![Selenium][Selenium.dev]][Selenium-url]


<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow the below steps.

1. Clone the repo
   ```sh
   git clone https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject.git
   ```
2. Install following packages
   * Fluent Assertions (6.7.0)
   * Microsoft.NET.Test.Sdk (17.3.1)
   * NUnit (3.13.3)
   * NUnit3TestAdapter (4.2.1)
   * Selenium.Support (4.4.0)
   * Selenium.WebDriver (4.4.0)
   * Selenium.WebDriver.ChromeDriver (105.0.5)
   * SpecFlow.NUnit (3.9.74)
   * SpecFlow.Plus.LivingDocPlugin (3.9.57)

3.Edit environment variables in the 'startEdgewordsTests.bat' batch file.
   ```sh
   SET email=YOUR_EMAIL
   SET password=YOUR_PASSWORD
   SET BROWSER=YOUR_BROWSER
   SET STEPSCREENSHOT=CONDITION(true/false)
   ```
<p align="middle">or</p>
3b.Alternatively can edit environment variables in a `.runsettings` file.
   ```xml
   <email>YOUR_EMAIL</email>
   <password>YOUR_PASSWORD</password>
   <BROWSER>YOUR_BROSWER</BROWSER>
   STEPSCREENSHOT=CONDITION(true/false);
   ```
4. Run the batch file.
   ```sh
   C:\...\nfocus.dylanwesthead.ecommerceproject> startEdgewordsTests.bat
   ```

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ACKNOWLEDGMENTS -->
## Acknowledgments

* [Specflow + Living Doc](https://docs.specflow.org/projects/specflow-livingdoc/)
* [GitHub Docs](https://docs.github.com/)
* [Choose a License](https://choosealicense.com/)

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/d-wezzer/nfocus.dylanwesthead.ecommerceproject.svg?style=for-the-badge
[contributors-url]: https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/d-wezzer/nfocus.dylanwesthead.ecommerceproject.svg?style=for-the-badge
[forks-url]: https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject/network/members
[stars-shield]: https://img.shields.io/github/stars/d-wezzer/nfocus.dylanwesthead.ecommerceproject.svg?style=for-the-badge
[stars-url]: https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject/stargazers
[issues-shield]: https://img.shields.io/github/issues/d-wezzer/nfocus.dylanwesthead.ecommerceproject.svg?style=for-the-badge
[issues-url]: https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject/issues
[license-shield]: https://img.shields.io/github/license/d-wezzer/nfocus.dylanwesthead.ecommerceproject.svg?style=for-the-badge
[license-url]: https://github.com/d-wezzer/nfocus.dylanwesthead.ecommerceproject/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/dylan-westhead/

[Dotnet.microsoft.com]: https://img.shields.io/badge/.net-702963?style=for-the-badge&logo=dotnet&logoColor=white
[Dotnet-url]: https://dotnet.microsoft.com/
[Selenium.dev]: https://img.shields.io/badge/selenium-00B900?style=for-the-badge&logo=Selenium&logoColor=white
[Selenium-url]: https://www.selenium.dev/
[Cucumber.io]: https://img.shields.io/badge/cucumber-329632?style=for-the-badge&logo=cucumber&logoColor=white
[Cucumber-url]: https://cucumber.io/
