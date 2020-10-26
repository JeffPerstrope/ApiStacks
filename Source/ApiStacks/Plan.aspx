<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="ApiStacks.Plan" MasterPageFile="~/dashboard.Master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="dashboardPanel" runat="server">
    <script src="https://js.stripe.com/v3/"></script>

    <div class="border-bottom pb-4">
        <div class="row">

            <div class="col-md-6 mt-4 pt-2 pt-sm-0">
                <h5>Premium Plans :</h5>

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
                        <%if (Session["userPlan"].ToString() == "starter")
                            { %>
                        <a class="btn btn-light">Current</a>
                        <%}
                            else if (Session["userPlan"].ToString() != "free")
                            { %>
                        <a class="btn btn-light">Contact to switch</a>
                        <%}
                            else
                            { %>
                        <button onclick="checkout('starter', '<%= Session["userID"].ToString().Trim()%>');" class="btn btn-primary">Buy</button>
                        <%} %>
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
                        <%if (Session["userPlan"].ToString() == "professional")
                            { %>
                        <a class="btn btn-light">Current</a>
                        <%}
                            else if (Session["userPlan"].ToString() != "free")
                            { %>
                        <a class="btn btn-primary">Contact to switch</a>
                        <%}
                            else
                            { %>
                        <button onclick="checkout('professional', '<%= Session["userID"].ToString().Trim()%>');" class="btn btn-primary">Buy</button>
                        <%} %>
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
                        <%if (Session["userPlan"].ToString() == "ultimate")
                            { %>
                        <a class="btn btn-light">Current</a>
                        <%}
                            else if (Session["userPlan"].ToString() != "free")
                            { %>
                        <a class="btn btn-primary">Contact to switch</a>
                        <%}
                            else
                            { %>
                        <button onclick="checkout('ultimate', '<%= Session["userID"].ToString().Trim()%>');" class="btn btn-primary">Buy</button>
                        <%} %>
                    </div>
                </div>


            </div>
            <!--end col-->



            <div class="col-md-6 mt-4 pt-2 pt-sm-0">
                <h5>Free Plans :</h5>

                <div class="media key-feature align-items-center p-3 rounded shadow mt-4">
                    <div class="media-body content ml-3">
                        <h4 class="title mb-0">Free Forever</h4>
                        <div class="d-flex mb-4">
                            <span class="h4 mb-0 mt-2">$</span>
                            <span class="price h1 mb-0">0</span>
                            <span class="h4 align-self-end mb-1">/mo</span>
                        </div>
                        <a class="text-primary">1,200 requests / mo</a>
                        <p class="text-muted mb-0">No Credit Card Required</p>
                        <%if (Session["userPlan"].ToString() == "free")
                            { %>
                        <a class="btn btn-light">Current</a>
                        <%}
                            else if (Session["userPlan"].ToString() != "free")
                            { %>
                        <a class="btn btn-primary">Contact to switch</a>
                        <%}
                            else
                            { %>
                        <button onclick="checkout('ultimate', '<%= Session["userID"].ToString().Trim()%>');" class="btn btn-primary">Switch</button>
                        <%} %>
                    </div>
                </div>
            </div>
            <!--end col-->

            <!--end col-->


        </div>
        <!--end row-->

        <br />
        <div id="modalAPIAppAlert" class="alert alert-warning" role="alert">To cancel your plan, contact us</div>
    </div>








    <script>

        function checkout(plan, userId) {

            $("#preloader").show();
            $("#status").show();

            var stripe = Stripe('pk_test_51HcZiyF6EVrg0l22sFpkZmUQZpmFzd4W5AEqF1rKbWtX9bX35yDvvFgzrDKI0i2xBrPJ7HHgyB7Mzt9OK2VxHSdR004PbSOz1D');

            fetch('https://api.apistacks.com/v1/create-checkout-session?plan=' + plan + '&userid=' + userId, {
                method: 'POST',
            })
                .then(function (response) {
                    return response.json();
                })
                .then(function (session) {
                    return stripe.redirectToCheckout({ sessionId: session.id });
                })
                .then(function (result) {
                    // If `redirectToCheckout` fails due to a browser or network
                    // error, you should display the localized error message to your
                    // customer using `error.message`.
                    if (result.error) {
                        alert(result.error.message);
                        $("#preloader").hide();
                        $("#status").hide();
                    }
                })
                .catch(function (error) {
                    console.error('Error:', error);
                    $("#preloader").hide();
                    $("#status").hide();
                });
        }
    </script>

</asp:Content>
