<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="ApiStacks.Plan" MasterPageFile="~/dashboard.Master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="dashboardPanel" runat="server">

    <div class="border-bottom pb-4">
        <div class="row">

            <div class="col-md-6 mt-4 pt-2 pt-sm-0">
                <h5>Switch Plan :</h5>

                <div class="media key-feature align-items-center p-3 rounded shadow mt-4">
                    <div class="media-body content ml-3">
                        <h4 class="title mb-0">Starter</h4>
                        <div class="d-flex mb-4">
                                <span class="h4 mb-0 mt-2">$</span>
                                <span class="price h1 mb-0">12.99</span>
                                <span class="h4 align-self-end mb-1">/mo</span>
                            </div>
                        <a class="text-primary">10,000 requests / mo</a>
                        <p class="text-muted mb-0">Payment through Stripe</p>
                        <a href="javascript:void(0)" class="btn btn-primary">Activate</a>
                    </div>
                </div>

                <div class="media key-feature align-items-center p-3 rounded shadow mt-4">
                    <div class="media-body content ml-3">
                        <h4 class="title mb-0">Professional</h4>
                        <div class="d-flex mb-4">
                                <span class="h4 mb-0 mt-2">$</span>
                                <span class="price h1 mb-0">29.99</span>
                                <span class="h4 align-self-end mb-1">/mo</span>
                            </div>
                        <a class="text-primary">35,000 requests / mo</a>
                        <p class="text-muted mb-0">Payment through Stripe</p>
                        <a href="javascript:void(0)" class="btn btn-primary">Activate</a>
                    </div>
                </div>

                <div class="media key-feature align-items-center p-3 rounded shadow mt-4">
                    <div class="media-body content ml-3">
                        <h4 class="title mb-0">Ultimate</h4>
                        <div class="d-flex mb-4">
                                <span class="h4 mb-0 mt-2">$</span>
                                <span class="price h1 mb-0">129.99</span>
                                <span class="h4 align-self-end mb-1">/mo</span>
                            </div>
                        <a class="text-primary">100,000 requests / mo</a>
                        <p class="text-muted mb-0">Payment through Stripe</p>
                        <a href="javascript:void(0)" class="btn btn-primary">Activate</a>
                    </div>
                </div>

                
            </div>
            <!--end col-->



            <div class="col-md-6 mt-4 pt-2 pt-sm-0">
                <h5>Current Plan :</h5>

                <div class="media key-feature align-items-center p-3 rounded shadow mt-4">
                    <div class="media-body content ml-3">
                        <h4 class="title mb-0">Starter</h4>
                        <div class="d-flex mb-4">
                                <span class="h4 mb-0 mt-2">$</span>
                                <span class="price h1 mb-0">0</span>
                                <span class="h4 align-self-end mb-1">/mo</span>
                            </div>
                        <a class="text-primary">1,200 requests / mo</a>
                        <p class="text-muted mb-0">Payment through Stripe</p>
                        <a href="javascript:void(0)" class="btn btn-light">Current</a>
                    </div>
                </div>
            </div>
            <!--end col-->

            <!--end col-->

            
        </div>
        <!--end row-->
    </div>

</asp:Content>
