<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Docs.aspx.cs" Inherits="ApiStacks.Docs" MasterPageFile="~/default.Master" ClientIDMode="Static" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
    <title>webscreenshots</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder_HeaderButton" runat="server">
    
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <body>
        <!-- Loader -->
        <!-- <div id="preloader">
            <div id="status">
                <div class="spinner">
                    <div class="double-bounce1"></div>
                    <div class="double-bounce2"></div>
                </div>
            </div>
        </div> -->
        <!-- Loader -->
        
        
        <!-- Hero Start -->
        <section class="bg-half bg-light d-table w-100">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-12 text-center">
                        <div class="page-next-level">
                            <h1 class="title"> Documentation </h1>
                            <div class="page-next">
                                <nav aria-label="breadcrumb" class="d-inline-block">
                                    <ul class="breadcrumb bg-white rounded shadow mb-0">
                                        <li class="breadcrumb-item"><a href="#">Quick Get Started Guide</a></li>
                                    </ul>
                                </nav>
                            </div>
                        </div>
                    </div>  <!--end col-->
                </div><!--end row-->
            </div> <!--end container-->
        </section><!--end section-->
        <!-- Hero End -->

        <!-- Shape Start -->
        <div class="position-relative">
            <div class="shape overflow-hidden text-white">
                <svg viewBox="0 0 2880 48" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path d="M0 48H1437.5H2880V0H2160C1442.5 52 720 0 720 0H0V48Z" fill="currentColor"></path>
                </svg>
            </div>
        </div>
        <!--Shape End-->

        <!-- Start -->
        <section class="section">
            <div class="container">
                <div class="row">
                    <div class="col-lg-3 col-md-4 col-12 d-none d-md-block">
                        <div class="sticky-bar component-wrapper bg-light rounded shadow p-4">
                            <p class="text-muted h6 mr-2">GETTING STARTED</p>
                            <ul class="list-unstyled mb-0">
                                <li><a href="#typography" class="active mouse-down h6 text-dark">Overview</a></li>
                                <li><a href="#display" class="mouse-down h6 text-dark">Dashboard Basics</a></li>
                                <li><a href="#bg-colors" class="mouse-down h6 text-dark">API Keys</a></li>
                            </ul>

                            <p style="margin-top: 30px" class="text-muted h6 mr-2" style="margin-top: 20px">WEB SCREENSHOTS</p>
                            <ul class="list-unstyled mb-0">
                                <li><a href="#display" class="mouse-down h6 text-dark">Getting Started</a></li>
                                <li><a href="#bg-colors" class="mouse-down h6 text-dark">Options List</a></li>
                            </ul>

                            <p style="margin-top: 30px" class="text-muted h6 mr-2" style="margin-top: 20px">WEB SCREENSHOTS</p>
                            <ul class="list-unstyled mb-0">
                                <li><a href="#display" class="mouse-down h6 text-dark">Getting Started</a></li>
                                <li><a href="#bg-colors" class="mouse-down h6 text-dark">Options List</a></li>
                            </ul>

                            <p style="margin-top: 30px" class="text-muted h6 mr-2" style="margin-top: 20px">WEB SCREENSHOTS</p>
                            <ul class="list-unstyled mb-0">
                                <li><a href="#display" class="mouse-down h6 text-dark">Getting Started</a></li>
                                <li><a href="#bg-colors" class="mouse-down h6 text-dark">Options List</a></li>
                            </ul>
                        </div>
                    </div>

                    <div class="col-lg-9 col-md-8 col-12 mt-4 pt-2 mt-sm-0 pt-sm-0">
                        <div class="row row-cols-1 ml-lg-2">   
                            <!-- OVERVIEW -->
                            <div class="col" id="typography">
                                <div class="component-wrapper rounded shadow">
                                    <div class="p-4 border-bottom">
                                        <h1> Overview </h1>
                                    </div>
        
                                    <div class="p-4">
                                        <p>Hi there, and welcome to ApiStacks!</p>
                                        <p>ApiStacks aims to provide very powerful and easy to use APIs to automate any web and browser functionality without having to worry about maintaining or building your backend infrastructure. We do all that for you.</p>
                                        <p>All you have to do is provide a URL or text, and the API will do the rest for you. Get easy to read JSON Responses like this:</p>
                                        <img src="/images/docs/doc_json.JPG" style="width: 100%; margin-bottom: 20px;""/>
                                        <p>This documentation will teach you all the basics on how to get started</p>
                                    </div>
                                </div>
                            </div><!--end col-->
                            <!-- Typography Heading End -->


                             <!-- DASHBOARD -->
                            <div class="col" id="dashboard">
                                <div class="component-wrapper rounded shadow">
                                    <div class="p-4 border-bottom">
                                        <h1> Dashboard Basics </h1>
                                    </div>
        
                                    <div class="p-4">
                                        <p>Once you <a target="_blank" href="/SignUp">Sign Up</a> for a free account, you will gain access to the Dashboard.</p>
                                        <p>This is where you manage your API apps, plan, and configuration. When you log in, it looks like this:</p>
                                        <img src="/images/docs/doc_dashboard.JPG" style="width: 100%; margin-bottom: 20px;""/>
                                        <p>You can view all the API Apps you have access to, view your API Keys, and change your settings</p>
                                    </div>
                                </div>
                            </div><!--end col-->
                            <!-- Typography Heading End -->


                            <!-- APIKEYS -->
                            <div class="col" id="apikeys">
                                <div class="component-wrapper rounded shadow">
                                    <div class="p-4 border-bottom">
                                        <h1> API Keys </h1>
                                    </div>
        
                                    <div class="p-4">
                                        <p>Before consuming any of the APIs, you will need to authenticate your requests with an API Key. Do do so, you will need to first <a target="_blank" href="/SignUp">Sign Up</a> for a free account, you will receive a free API Key to use.</p>
                                        <p>To get your API Key, sign into your account and click the small (i) on the app you wish to consume. A popup like this will appear:</p>
                                        <img src="/images/docs/doc_apikey.JPG" style="width: 100%; margin-bottom: 20px;"/>
                                        <p>Remember, your API Key is private and should not be shared! You don't want your key to be abused. Always keep it safe!</p>
                                        <p>To use this key, simply add your key to the request header as <a style="">x-api-key</a></p>
                                    </div>
                                </div>
                            </div><!--end col-->
                            <!-- Typography Heading End -->

                        </div><!--end row-->
                    </div>
                </div>                
            </div><!--end container-->
        </section><!--end section-->
        <!-- End -->

        <!-- Back to top -->
        <a href="#" class="btn btn-icon btn-soft-primary back-to-top"><i data-feather="arrow-up" class="icons"></i></a>
        <!-- Back to top -->

        <!-- javascript -->
        <script src="js/jquery-3.5.1.min.js"></script>
        <script src="js/bootstrap.bundle.min.js"></script>
        <script src="js/jquery.easing.min.js"></script>
        <script src="js/scrollspy.min.js"></script>
        <!-- Icons -->
        <script src="js/feather.min.js"></script>
        <script src="https://unicons.iconscout.com/release/v2.1.9/script/monochrome/bundle.js"></script>
        <!-- Main Js -->
        <script src="js/app.js"></script>
    </body>
    </asp:Content>