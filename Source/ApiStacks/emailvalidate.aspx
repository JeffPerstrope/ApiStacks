<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="emailvalidate.aspx.cs" Inherits="ApiStacks.emailvalidate" MasterPageFile="~/default.Master" ClientIDMode="Static" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_Head" runat="server">
    <title>webscreenshots</title>
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
                        <h1 class="heading mb-3">Email Validator API</h1>

                        <div class="alert alert-light alert-pills shadow" role="alert">
                            <span class="badge badge-pill badge-danger mr-1">0.1.2 BETA</span>
                            <span class="content">Start <span class="text-primary">building</span> with this API</span>
                        </div>
                        <p class="para-desc mx-auto text-muted">Turn websites into high definition screenshot images super quick and super easy! Give it a try</p>


                        <%--Give it a try--%>
                        <form runat="server" id="webScnreenshotsTry">
                            <i data-feather="link" class="fea icon-sm icons" style="position: absolute; margin-left: 15px; margin-top: 10px"></i>
                            <input runat="server" id="txtAddress" type="email" class="form-control pl-5" style="display: inline-block; max-width: 500px; margin-bottom: 10px" placeholder="Email Address" required="">
                            <button id="btnTakeScreenshot" type="submit" onclick="takeScreenshot();" class="btn btn-primary" style="display: inline-block;"> <span id="spinner"></span> Validate</button>
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

                                        <div style="height: 400px;">
                                            <div  id="txtData2" runat="server" class="codeReader"></div>
                                        </div>
                                    </div>
                                </div>
                                <!--end col-->
                            </div>
                            <!--end row-->
                        </div>
                        <!--end container-->
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
            <div class="row">
                <div class="col-md-4 col-12">
                    <div class="features">
                        <div class="image position-relative d-inline-block">
                            <img src="images/icon/user.svg" class="avatar avatar-small" alt="">
                        </div>

                        <div class="content mt-4">
                            <h4 class="title-2">Easy To Use</h4>
                            <p class="text-muted mb-0">Nisi aenean vulputate eleifend tellus vitae eleifend enim a Aliquam eleifend aenean elementum semper.</p>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-md-4 col-12 mt-5 mt-sm-0">
                    <div class="features">
                        <div class="image position-relative d-inline-block">
                            <img src="images/icon/calendar.svg" class="avatar avatar-small" alt="">
                        </div>

                        <div class="content mt-4">
                            <h4 class="title-2">Daily Reports</h4>
                            <p class="text-muted mb-0">Allegedly, a Latin scholar established the origin of the established text by compiling unusual word.</p>
                        </div>
                    </div>
                </div>
                <!--end col-->

                <div class="col-md-4 col-12 mt-5 mt-sm-0">
                    <div class="features">
                        <div class="image position-relative d-inline-block">
                            <img src="images/icon/sand-clock.svg" class="avatar avatar-small" alt="">
                        </div>

                        <div class="content mt-4">
                            <h4 class="title-2">Real Time Zone</h4>
                            <p class="text-muted mb-0">It seems that only fragments of the original text remain in only fragments the Lorem Ipsum texts used today.</p>
                        </div>
                    </div>
                </div>
                <!--end col-->
            </div>
        </div>
        <!--end container-->
    </section>
    <!--end section-->
    <!-- Partners End -->

    <!-- Feature Start -->
        <div class="container mt-100 mt-60">
            <div class="row align-items-center">
                <div class="col-lg-5 col-md-6">
                    <img src="images/illustrator/Startup_SVG.svg" alt="">
                </div>
                <!--end col-->

                <div class="col-lg-6 col-md-6 mt-4 mt-sm-0 pt-2 pt-sm-0">
                    <div class="section-title ml-lg-5">
                        <h4 class="title mb-4">High performance and efficiency</h4>
                        <p class="text-muted">Due to its widespread use as filler text for layouts, non-readability is of great importance: human perception is tuned to recognize certain patterns and repetitions in texts. If the distribution of letters visual impact.</p>
                        <ul class="list-unstyled text-muted">
                            <li class="mb-0"><span class="text-primary h5 mr-2"><i class="uim uim-check-circle"></i></span>Digital Marketing Solutions for Tomorrow</li>
                            <li class="mb-0"><span class="text-primary h5 mr-2"><i class="uim uim-check-circle"></i></span>Create your own skin to match your brand</li>
                        </ul>
                        <a href="javascript:void(0)" class="mt-3 h6 text-primary">Find Out More <i class="mdi mdi-chevron-right"></i></a>
                    </div>
                </div>
                <!--end col-->
            </div>
            <!--end row-->
        </div>
        <!--end container-->

        <div class="container mt-100 mt-60">
            <div class="row align-items-center">
                <div class="col-lg-7 col-md-6 order-2 order-md-1 mt-4 mt-sm-0 pt-2 pt-sm-0">
                    <div class="section-title mr-lg-5">
                        <h4 class="title mb-4">Customizable screenshot features</h4>
                        <p class="text-muted">This prevents repetitive patterns from impairing the overall visual impression and facilitates the comparison of different typefaces. Furthermore, it is advantageous when the dummy text is relatively realistic.</p>
                        <ul class="list-unstyled text-muted">
                            <li class="mb-0"><span class="text-primary h5 mr-2"><i class="uim uim-check-circle"></i></span>Custom File Format</li>
                            <li class="mb-0"><span class="text-primary h5 mr-2"><i class="uim uim-check-circle"></i></span>Partial or Full Page screenshot</li>
                            <li class="mb-0"><span class="text-primary h5 mr-2"><i class="uim uim-check-circle"></i></span>Device type emulation</li>
                            <li class="mb-0"><span class="text-primary h5 mr-2"><i class="uim uim-check-circle"></i></span>Inject CSS and JS into page</li>
                        </ul>
                        <a href="javascript:void(0)" class="mt-3 h6 text-primary">Documentation <i class="mdi mdi-chevron-right"></i></a>
                    </div>
                </div>
                <!--end col-->

                <div class="col-lg-5 col-md-6 order-1 order-md-2">
                    <img src="images/illustrator/app_development_SVG.svg" alt="">
                </div>
                <!--end col-->
            </div>
            <!--end row-->
        </div>
        <!--end container-->




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
