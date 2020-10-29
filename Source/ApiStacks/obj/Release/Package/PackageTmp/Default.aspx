<%@ Page Title="" Language="C#" MasterPageFile="~/default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ApiStacks.WebForm1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
    <title>ApiStacks - Restful Easy To Use APIs</title>
</asp:Content>


<asp:Content ContentPlaceHolderID="ContentPlaceHolder_HeaderButton" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Hero Start -->
    <section class="bg-half-170  d-table w-100" id="home">
        <div class="container">
            <div class="row align-items-center">
                <div class="col-lg-7 col-md-7">
                    <div class="title-heading mt-4">
                        <h1 class="heading mb-3">Automate your business with Fast, Scalable, and Reliable
                            <br>
                            <span class="element text-primary" data-elements="APIs, Webapps, Data"></span></h1>
                        <p class="para-desc text-muted">Don't re-invent the wheel. Save time and money by implementing these Web APIs to your product and get to market faster! 😎</p>
                        <div class="mt-4">
                            <%if (Session["userID"] == null)
                                { %>
                            <a href="\SignUp.aspx" class="btn btn-primary mt-2 mr-2"><i class="mdi mdi-account"></i>Sign up for free</a>
                            <% }
                                else
                                { %>
                            <a href="\Dashboard.aspx" class="btn btn-primary">Visit Dashboard</a>
                            <% } %>


                            <%--<a href="#sectionPricing" class="btn btn-outline-primary mt-2"><i class="mdi mdi-currency-usd"></i>Pricing</a>--%>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-5 col-md-5 mt-4 pt-2 mt-sm-0 pt-sm-0">
                    <img src="images/illustrator/Startup_SVG.svg" alt="">
                </div>
                <!--end col-->
            </div>
            <!--end row-->
        </div>
        <!--end container-->
    </section>
    <!--end section-->
    <!-- Hero End -->

    <!-- Partners start -->
    <section class="py-4 border-bottom border-top">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-2 col-md-2 col-6 text-center py-4">
                    <img src="images/technologies/1.png" class="avatar avatar-ex-sm" alt="">
                </div>
                <!--end col-->

                <div class="col-lg-2 col-md-2 col-6 text-center py-4">
                    <img src="images/technologies/2.png" class="avatar avatar-ex-sm" alt="">
                </div>
                <!--end col-->

                <div class="col-lg-2 col-md-2 col-6 text-center py-4">
                    <img src="images/technologies/3.png" class="avatar avatar-ex-sm" alt="">
                </div>
                <!--end col-->

                <div class="col-lg-2 col-md-2 col-6 text-center py-4">
                    <img src="images/technologies/4.png" class="avatar avatar-ex-sm" alt="">
                </div>
                <!--end col-->

                <div class="col-lg-2 col-md-2 col-6 text-center py-4">
                    <img src="images/technologies/5.png" class="avatar avatar-ex-sm" alt="">
                </div>
                <!--end col-->

                <div class="col-lg-2 col-md-2 col-6 text-center py-4">
                    <img src="images/technologies/6.png" class="avatar avatar-ex-sm" alt="">
                </div>
                <!--end col-->
            </div>
            <!--end row-->
        </div>
        <!--end container-->
    </section>
    <!--end section-->
    <!-- Partners End -->


    <!-- Stats Start -->
    <div class="container mt-100 mt-60">
        <div class="row justify-content-center">
            <div class="col-12 text-center">
                <div class="section-title mb-4 pb-2">
                    <h4 class="title mb-4">We are growing, how about you grow <span class="text-primary">with us</span></h4>
                    <p class="text-muted para-desc mx-auto mb-0">Since <span class="text-primary font-weight-bold">launching</span> we have been seeing awesome growth and activity! Take a look for yourself</p>
                </div>
            </div>
            <!--end col-->
        </div>
        <!--end row-->

        <div class="row" id="counter">
            <div class="col-md-3 col-6 mt-4 pt-2">
                <div class="counter-box text-center">
                    <img src="images/illustrator/Asset190.svg" class="avatar avatar-small" alt="">
                    <h2 class="mb-0 mt-4"><span class="counter-value" data-count="18500">1</span>+</h2>
                    <h6 class="counter-head text-muted">API Invocations</h6>
                </div>
                <!--end counter box-->
            </div>

            <div class="col-md-3 col-6 mt-4 pt-2">
                <div class="counter-box text-center">
                    <img src="images/illustrator/Asset189.svg" class="avatar avatar-small" alt="">
                    <h2 class="mb-0 mt-4"><span class="counter-value" data-count="4">1</span>GB+</h2>
                    <h6 class="counter-head text-muted">Data Served</h6>
                </div>
                <!--end counter box-->
            </div>

            <div class="col-md-3 col-6 mt-4 pt-2">
                <div class="counter-box text-center">
                    <img src="images/illustrator/Asset186.svg" class="avatar avatar-small" alt="">
                    <h2 class="mb-0 mt-4"><span class="counter-value" data-count="99">1</span>%</h2>
                    <h6 class="counter-head text-muted">Uptime</h6>
                </div>
                <!--end counter box-->
            </div>

            <div class="col-md-3 col-6 mt-4 pt-2">
                <div class="counter-box text-center">
                    <img src="images/illustrator/Asset187.svg" class="avatar avatar-small" alt="">
                    <h2 class="mb-0 mt-4"><span class="counter-value" data-count="20">0</span>+</h2>
                    <h6 class="counter-head text-muted">Implementations</h6>
                </div>
                <!--end counter box-->
            </div>
        </div>
        <!--end row-->
    </div>
    <!--end container-->
    <!-- Stats End -->


    <!-- List of APIs -->
    <section class="section bg-light" id="sectionAPI">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-12 text-center">
                    <div class="section-title mb-4 pb-2">
                        <h6 class="text-primary">Products</h6>
                        <h4 class="title mb-4">Explore our API products</h4>
                        <p class="text-muted para-desc mb-0 mx-auto">We've done the hard part so you don't have to! Check out our <span class="text-primary font-weight-bold">API Products</span> and get started with very little effort</p>
                    </div>
                </div>
                <!--end col-->
            </div>
            <!--end row-->

            <div class="row">
                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/webscreenshots.png" style="height: 60px;" />
                            <%--<i class="uil uil-airplay"></i>--%>
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Web Screenshots</a></h5>
                            <p class="text-muted">Turn websites into high definition screenshot images super quick and super easy!</p>

                            <a href="/webscreenshots" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/webpdf.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Web PDF</a></h5>
                            <p class="text-muted">Convert any website link into a downloadable and printable PDF document that can be customized</p>

                            <a href="/webpdf" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/opengraph.png" style="height: 60px;" />
                            <%--<i class="uil uil-airplay"></i>--%>
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Open Graph Scraper</a></h5>
                            <p class="text-muted">Extract open graph and social details from any website or URL with just one line of code</p>

                            <a href="/opengraph" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->


                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/languagedetect.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Language Detect</a></h5>
                            <p class="text-muted">Feed this API a few sentences and have it determine what language it is with a confidence score</p>

                            <a href="/languagedetect" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/webscraper.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Web Scraper</a></h5>
                            <p class="text-muted">Automatically scrape a website's HTML source by providing a link to a website or specific page</p>

                            <a href="/webscraper" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/emailvalidate.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Email Validator</a></h5>
                            <p class="text-muted">Stop guessing if an email is valid or full. With this API, you can determine first hand if the email is deliverable and working</p>

                            <a href="/emailvalidate" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->


                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/qrcode.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">QR Code Generator</a></h5>
                            <p class="text-muted">Turn any URL or even text into a downloadable and printable QR code with ease.</p>

                            <a href="/qrcode" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>
                <!--end col-->


                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/opengraph.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">WHOIS Lookup</a></h5>
                            <p class="text-muted">Quick and easy way to check domain WHOIS data and know who it is registered to, when it was registered, and much more </p>

                            <a href="/whois" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>


                <div class="col-lg-4 col-md-6 col-12 mt-4 pt-2">
                    <div class="card features explore-feature p-4 px-md-3 border-0 rounded-md shadow text-center">
                        <div class="icons rounded h2 text-center text-primary px-3">
                            <img src="images/app/linkscraper.png" style="height: 60px;" />
                        </div>

                        <div class="card-body p-0 content">
                            <h5 class="mt-4"><a href="javascript:void(0)" class="title text-dark">Link Scraper</a></h5>
                            <p class="text-muted">Extract all links found on a web page by simply providing a URL to this API. Super fast and easy to use</p>

                            <a href="/linkscraper" class="text-primary">Try It <i data-feather="chevron-right" class="fea icon-sm"></i></a>
                        </div>
                    </div>
                </div>



                <!--end col-->
            </div>
            <!--end row-->
        </div>
        <!--end container-->
    </section>




    <!-- javascript -->
    <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/scrollspy.min.js"></script>
    <!-- Typed -->
    <script src="js/typed.js"></script>
    <script src="js/typed.init.js"></script>
    <!-- Counter -->
    <script src="js/counter.init.js "></script>
    <!-- SLIDER -->
    <script src="js/owl.carousel.min.js "></script>
    <script src="js/owl.init.js "></script>
    <!-- Icons -->
    <script src="js/feather.min.js"></script>
    <script src="https://unicons.iconscout.com/release/v2.1.9/script/monochrome/bundle.js"></script>
    <!-- Main Js -->
    <script src="js/app.js"></script>

    <style>
        .avatar.avatar-ex-sm {
            max-height: 50px !important;
        }
    </style>
</asp:Content>
