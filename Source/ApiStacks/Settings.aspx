<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="ApiStacks.Settings" MasterPageFile="~/dashboard.Master" %>



<asp:Content ID="Content2" ContentPlaceHolderID="dashboardPanel" runat="server">
    <h5>Settings:</h5>
    <form>
        <div class="row mt-4">
            <div class="col-md-6">
                <div class="form-group position-relative">
                    <label>First Name</label>
                    <i data-feather="user" class="fea icon-sm icons"></i>
                    <input name="name" id="first" type="text" class="form-control pl-5" placeholder="First Name :">
                </div>
            </div>
            <!--end col-->
            <div class="col-md-6">
                <div class="form-group position-relative">
                    <label>Last Name</label>
                    <i data-feather="user-check" class="fea icon-sm icons"></i>
                    <input name="name" id="last" type="text" class="form-control pl-5" placeholder="Last Name :">
                </div>
            </div>
            <!--end col-->
            <div class="col-md-6">
                <div class="form-group position-relative">
                    <label>Your Email</label>
                    <i data-feather="mail" class="fea icon-sm icons"></i>
                    <input name="email" id="email" type="email" class="form-control pl-5" placeholder="Your email :">
                </div>
            </div>
            <!--end col-->
            <div class="col-md-6">
                <div class="form-group position-relative">
                    <label>Occupation</label>
                    <i data-feather="bookmark" class="fea icon-sm icons"></i>
                    <input name="name" id="business" type="text" class="form-control pl-5" placeholder="Business :">
                </div>
            </div>
            <!--end col-->

        </div>
        <!--end row-->
        <div class="row">
            <div class="col-sm-12">
                <input type="submit" id="submit" name="send" class="btn btn-primary" value="Save Changes">
            </div>
            <!--end col-->
        </div>
        <!--end row-->
    </form>
    <!--end form-->


    <div class="row">
        <div class="col-md-6 mt-4 pt-2">
            <h5>Contact Info :</h5>

            <form>
                <div class="row mt-4">
                    <div class="col-lg-12">
                        <div class="form-group position-relative">
                            <label>Phone No. :</label>
                            <i data-feather="phone" class="fea icon-sm icons"></i>
                            <input name="number" id="number" type="number" class="form-control pl-5" placeholder="Phone :">
                        </div>
                    </div>
                    <!--end col-->

                    <div class="col-lg-12">
                        <div class="form-group position-relative">
                            <label>Website :</label>
                            <i data-feather="globe" class="fea icon-sm icons"></i>
                            <input name="url" id="url" type="url" class="form-control pl-5" placeholder="Url :">
                        </div>
                    </div>
                    <!--end col-->

                    <div class="col-lg-12 mt-2 mb-0">
                        <button class="btn btn-primary">Add</button>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </form>
        </div>
        <!--end col-->

        <div class="col-md-6 mt-4 pt-2">
            <h5>Change password :</h5>
            <form>
                <div class="row mt-4">
                    <div class="col-lg-12">
                        <div class="form-group position-relative">
                            <label>Old password :</label>
                            <i data-feather="key" class="fea icon-sm icons"></i>
                            <input type="password" class="form-control pl-5" placeholder="Old password" required="">
                        </div>
                    </div>
                    <!--end col-->

                    <div class="col-lg-12">
                        <div class="form-group position-relative">
                            <label>New password :</label>
                            <i data-feather="key" class="fea icon-sm icons"></i>
                            <input type="password" class="form-control pl-5" placeholder="New password" required="">
                        </div>
                    </div>
                    <!--end col-->

                    <div class="col-lg-12">
                        <div class="form-group position-relative">
                            <label>Re-type New password :</label>
                            <i data-feather="key" class="fea icon-sm icons"></i>
                            <input type="password" class="form-control pl-5" placeholder="Re-type New password" required="">
                        </div>
                    </div>
                    <!--end col-->

                    <div class="col-lg-12 mt-2 mb-0">
                        <button class="btn btn-primary">Save password</button>
                    </div>
                    <!--end col-->
                </div>
                <!--end row-->
            </form>
        </div>
        <!--end col-->
    </div>
    <!--end row-->

</asp:Content>
