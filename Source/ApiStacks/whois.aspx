<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whois.aspx.cs" Inherits="ApiStacks.whois" MasterPageFile="~/default.Master" ClientIDMode="Static" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
    <title>WHOIS Lookup | ApiStacks Restful API</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder_HeaderButton" runat="server">
    
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <!-- Hero Start -->
    <section class="bg-home d-flex align-items-center" style="background: url('images/saas/home-shape.png') center center; height: auto;" id="home">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-lg-12 text-center mt-0 mt-md-5 pt-0 pt-md-5">
                    <div class="title-heading margin-top-100">
                        <h1 class="heading mb-3">WHOIS Lookup API</h1>

                        <div class="alert alert-light alert-pills shadow" role="alert">
                            <span class="badge badge-pill badge-danger mr-1">0.1.2 BETA</span>
                            <span class="content">Start <span class="text-primary">building</span> with this API</span>
                        </div>
                        <p class="para-desc mx-auto text-muted">Quick and easy way to check domain WHOIS data and know who it is registered to, when it was registered, and much more. Give it a try </p>


                        <%--Give it a try--%>
                        <form runat="server" id="webScnreenshotsTry">
                            <i data-feather="link" class="fea icon-sm icons" style="position: absolute; margin-left: 15px; margin-top: 10px"></i>
                            <input runat="server" id="txtAddress" type="text" class="form-control pl-5" style="display: inline-block; max-width: 500px" placeholder="Web Address" required="">
                            <button id="btnTakeScreenshot" type="submit" onclick="takeScreenshot();" class="btn btn-primary" style="display: inline-block;"><span id="spinner"></span>Lookup</button>
                        </form>
                    </div>



                    <%--CODE EDITOR--%>
                        <div class="container" style="margin-top: 50px;">
                            <div class="row justify-content-center">
                                <div class="col-lg-9">
                                    <div class="p-4 shadow rounded border">
                                        <div style="opacity: 0.6;">
                                            <div style="width: 10px; height: 10px; background-color: red; border-radius: 5px; display: inline-block"></div>
                                            <div style="width: 10px; height: 10px; background-color: yellow; border-radius: 5px; display: inline-block"></div>
                                            <div style="width: 10px; height: 10px; background-color: greenyellow; border-radius: 5px; display: inline-block"></div>
                                        </div>
                                        <div style="height: 400px;">
                                            <textarea readonly id="txtData" style="font-family: 'Space Mono', monospace; font-size: 13px" runat="server" class="codeReader"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <!--end row-->
                        </div>
                        <!--end container-->



                    <%--<div class="home-dashboard">
                        <img runat="server" id="screenshotImage" src="images/webscreenshots_Browser.jpeg" alt="" class="img-fluid">
                    </div>--%>
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
    <section class="section bg-light mt-0 mt-md-5">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-12 text-center">
                    <div class="section-title mb-4 pb-2">
                        <h4 class="title mb-4">Send us the URL, we'll do the rest 😎</h4>
                        <p class="text-muted para-desc mb-0 mx-auto">We've done all the hard work, so you don't have to.</p>
                    </div>
                </div>
                <!--end col-->
            </div>

            <div class="row">
                <div class="col-md-4 col-12">
                    <div class="features">
                        <div class="image position-relative d-inline-block">
                            <img src="images/icon/24-hours.svg" class="avatar avatar-small" alt="">
                        </div>

                        <div class="content mt-4">
                            <h4 class="title-2">Always Up</h4>
                            <p class="text-muted mb-0">Now you don't have to worry about uptime and managing servers. This API will be ready to go as soon as you request it</p>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-md-4 col-12 mt-5 mt-sm-0">
                    <div class="features">
                        <div class="image position-relative d-inline-block">
                            <img src="images/icon/speedometer.svg" class="avatar avatar-small" alt="">
                        </div>

                        <div class="content mt-4">
                            <h4 class="title-2">Scalable CDN</h4>
                            <p class="text-muted mb-0">This API is distributed and served through a Content Delivery Network, ensuring very fast and reliable data</p>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-md-4 col-12 mt-5 mt-sm-0">
                    <div class="features">
                        <div class="image position-relative d-inline-block">
                            <img src="images/icon/code.svg" class="avatar avatar-small" alt="">
                        </div>

                        <div class="content mt-4">
                            <h4 class="title-2">Customizable</h4>
                            <p class="text-muted mb-0">Customize your API requests to ensure that you get data that suits your needs perfectly, everytime</p>
                        </div>
                    </div>
                </div>
                <!--end col-->
            </div>
        </div>
        <!--end container-->
    </section>
    <!-- Partners End -->

    <!-- Start Services -->
        <section class="section">
            <div class="container pb-lg-4 mb-md-5 mb-4">
                <div class="row align-items-center mb-4">
                    <div class="col-lg-9 col-md-8 text-sm-left">
                        <div class="section-title">
                            <h2 style="font-weight: 800;" >High Performance</h2>
                            <h2 style="font-weight: 800; color: red" >with Zero Compromise</h2>
                            <p class="text-muted para-desc mb-0">Don't worry about server maintenance, load balancing, or spending hours building and maintaining these services. <span class="text-primary font-weight-bold">ApiStacks</span> takes out all the hassle, giving you more time to build!</p>
                        </div>
                    </div><!--end col-->

                    <div class="col-lg-3 col-md-4 mt-4 mt-sm-0 text-sm-right pt-2 pt-sm-0">
                        <a href="/docs" target="_blank" class="btn btn-outline-primary">Read docs <i class="mdi mdi-chevron-right"></i></a>
                    </div><!--end col-->
                </div><!--end row-->

                <div class="row">
                    <div class="col-lg-4 col-md-6 col-12 mt-5 pt-3">
                        <div class="features">
                            <div class="image position-relative d-inline-block">
                                <img src="images/icon/computer.svg" class="avatar avatar-small" alt="">
                            </div>

                            <div class="content mt-4">
                                <h4 class="title-2">URL WHOIS Lookup</h4>
                                <p class="text-muted">Simply provide a URL and the API will retrieve WHOIS information regarding the website you provided in neat JSON format.</p>
                            </div>
                        </div>
                    </div><!--end col-->
                    
                    <div class="col-lg-4 col-md-6 col-12 mt-5 pt-3">
                        <div class="features">
                            <div class="image position-relative d-inline-block">
                                <img src="images/icon/cloud.svg" class="avatar avatar-small" alt="">
                            </div>

                            <div class="content mt-4">
                                <h4 class="title-2">Simple, Fast, Easy</h4>
                                <p class="text-muted">Requested WHOIS data will be processed by fast and secure Server Side lookups, delivering your results in seconds</p>
                            </div>
                        </div>
                    </div><!--end col-->
                    
                    <div class="col-lg-4 col-md-6 col-12 mt-5 pt-3">
                        <div class="features">
                            <div class="image position-relative d-inline-block">
                                <img src="images/icon/server.svg" class="avatar avatar-small" alt="">
                            </div>

                            <div class="content mt-4">
                                <h4 class="title-2">... and more</h4>
                                <p class="text-muted">You can specify how many redirects you want to follow, get a quick summary, or all the details on a specific website</p>
                            </div>
                        </div>
                    </div><!--end col-->
                </div><!--end row-->
            </div><!--end container-->
        </section><!--end section-->
        


    <!-- More About This API Start -->
        <section class="section" style="margin-top: -150px; margin-bottom: -80px">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-12 text-center">
                        <div class="section-title mb-4 pb-2">
                            <h4 class="title mb-4">More about this <span class="text-primary">API</span></h4>
                            <p class="text-muted para-desc mx-auto mb-0">Here are all the details you need to know about this <span class="text-primary font-weight-bold">API</span></p>
                        </div>
                    </div><!--end col-->
                </div><!--end row-->

               
            </div><!--end container-->

            <div class="container mt-100 mt-60" style="margin-top: -30px">
                <div class="row align-items-center">
                    <div class="col-lg-7 col-md-6">
                        <div class="faq-content mr-lg-5">
                            <div class="accordion" id="accordionExampleone">
                                <div class="card border-0 rounded mb-2">
                                    <a data-toggle="collapse" href="#collapseone" class="faq position-relative" aria-expanded="true" aria-controls="collapseone">
                                        <div class="card-header border-0 bg-light p-3 pr-5" id="headingfifone">
                                            <h6 class="title mb-0"> What is this API?</h6>
                                        </div>
                                    </a>
                                    <div id="collapseone" class="collapse show" aria-labelledby="headingfifone" data-parent="#accordionExampleone">
                                        <div class="card-body px-2 py-4">
                                            <p class="text-muted mb-0 faq-ans">WHOIS Lookup is a service that retrieves domain registration info based on the URL you provide. That's it.</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="card border-0 rounded mb-2">
                                    <a data-toggle="collapse" href="#collapsetwo" class="faq position-relative collapsed" aria-expanded="false" aria-controls="collapsetwo">
                                        <div class="card-header border-0 bg-light p-3 pr-5" id="headingtwo">
                                            <h6 class="title mb-0"> How does it work? </h6>
                                        </div>
                                    </a>
                                    <div id="collapsetwo" class="collapse" aria-labelledby="headingtwo" data-parent="#accordionExampleone">
                                        <div class="card-body px-2 py-4">
                                            <p class="text-muted mb-0 faq-ans">All you need to do is give it the URL that you want to see WHOIS registration information.</p>
                                        </div>
                                    </div>
                                </div>

                                <div class="card border-0 rounded mb-2">
                                    <a data-toggle="collapse" href="#collapsethree" class="faq position-relative collapsed" aria-expanded="false" aria-controls="collapsethree">
                                        <div class="card-header border-0 bg-light p-3 pr-5" id="headingthree">
                                            <h6 class="title mb-0"> Why not run my own solution? </h6>
                                        </div>
                                    </a>
                                    <div id="collapsethree" class="collapse" aria-labelledby="headingthree" data-parent="#accordionExampleone">
                                        <div class="card-body px-2 py-4">
                                            <p class="text-muted mb-0 faq-ans">Instead of worrying about server maintenance, server costs, and running your own infrastructure, <span class="text-primary font-weight-bold">ApiStacks</span> turns all that into an easy to use API endpoint.</p>
                                        </div>
                                    </div>
                                </div>

                                
                            </div>
                        </div>
                    </div><!--end col-->

                    <div class="col-lg-5 col-md-6 mt-4 mt-sm-0 pt-2 pt-sm-0">
                        <img src="images/illustrator/faq.svg" alt="">
                    </div><!--end col-->
                </div><!--end row-->
            </div><!--end container-->
        </section><!--end section-->
    <!-- More About This END -->


    <!-- javascript -->
    <script src="js/jquery-3.5.1.min.js"></script>
    <script src="js/bootstrap.bundle.min.js"></script>
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/scrollspy.min.js"></script>
    <!-- SLIDER -->
    <script src="js/owl.carousel.min.js "></script>
    <script src="js/owl.init.js "></script>
    <!-- Icons -->
    <script src="js/feather.min.js"></script>
    <script src="https://unicons.iconscout.com/release/v2.1.9/script/monochrome/bundle.js"></script>
    <!-- Main Js -->
    <script src="js/app.js"></script>


    <script>

        $(document).ready(function () {
            prettyPrint();
        });

        function takeScreenshot() {
            var address = $("#txtAddress").val().trim();
            
            if ($("#txtAddress").val().trim() != "") {
                $("#spinner").addClass("fa fa-circle-o-notch fa-spin");
                $("#spinner").css("margin-right", "9px");
                $("#spinner").css("cursor", "progress");
                //$("#btnTakeScreenshot").prop("disabled", true);
            }
        }


        function prettyPrint() {
            
            var ugly = $("#txtData").val();
            var obj = JSON.parse(ugly);
            var pretty = JSON.stringify(obj, undefined, 4);
            $("#txtData").val(pretty);
        }
    </script>
</asp:Content>
