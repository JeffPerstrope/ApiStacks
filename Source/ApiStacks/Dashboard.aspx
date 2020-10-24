<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="ApiStacks.Dashboard" MasterPageFile="~/dashboard.Master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="dashboardPanel" runat="server">
    <form runat="server">
        <h5>API Apps:</h5>

        <div class="row">



            <% foreach (var app in ApiStacks.AppsList.InstalledApps)
                { %>
            <div class="col-lg-6 col-md-4 mt-4 pt-2">
                <div class="text-center">
                    <img src="<%= app.icon %>" style="height: 50px" alt="">
                    <div class="content mt-3">
                        <a class="h5 text-dark"><%= app.name %></a>
                        <ul class="list-unstyled social-icon social mb-0 mt-2">
                            <li class="list-inline-item"><a onclick="showConfigModal('<%= app.name %>','<%= app.id %>', '<%= app.enabled %>');" class="rounded"><i data-feather="info" class="fea icon-sm fea-social"></i></a></li>
                            <li class="list-inline-item"><a href="javascript:void(0)" class="rounded"><i data-feather="help-circle" class="fea icon-sm fea-social"></i></a></li>
                        </ul>
                        <!--end icon-->
                        <% if (app.enabled)
                            { %>
                        <a href="/Dashboard?deactivate=<%= app.id %>" id="<%= app.id %>" class="btn btn-light">Deactivate</a>
                        <%}
                        else
                        {
                            if ((bool)Session["userMaxAppsReached"] == true)
                            { %>

                        <a data-toggle="modal" data-target="#modalAppLimitReached" class="btn btn-primary">Activate</a>


                        <%}
                        else
                        { %>
                        <a href="/Dashboard?activate=<%= app.id %>" id="<%= app.id %>" class="btn btn-primary">Activate</a>
                        <%}
                        }%>
                    </div>
                </div>
            </div>
            <!--end col-->




            <%--Generate Modal windows--%>
            <!-- Modal -->
            <div id="modal<%= app.id %>" class="modal fade" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 id="modalAPIAppTitle" class="modal-title"><%= app.name %> API</h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>

                        </div>
                        <div class="modal-body">
                            <h6 style="font-weight: 800">API Status:</h6>
                            <% if (app.enabled)
                                { %>
                            <div id="modalAPIAppAlert" class="alert alert-light" role="alert">API endpoint is currently active </div>
                            <%}
                                else
                                { %>
                            <div id="modalAPIAppAlert" class="alert alert-warning" role="alert">API endpoint inactive. Requests to this API will fail </div>
                            <%} %>


                            <hr />
                            <h6 style="font-weight: 800">Authentication:</h6>
                            <p class="text-muted" style="font-size: 13px">When authenticating your requests, pass the API Key in the request header as <a class="text-danger" style="font-weight: 800">'x-api-key' </a>:</p>
                            <p><a class="text-primary"><%= Session["userKey"].ToString() %></a></p>

                            <hr />
                            <h6 style="font-weight: 800">Endpoint:</h6>
                            <p class="text-primary">https://api.apistacks.com/v1/<a class="text-seconday"><%= app.endpoint %></a>?url={data}</p>


                            <hr />
                            <h6 style="display: inline-block; font-weight: 800">Customization:  </h6>
                            <span class="badge badge-pill badge-danger">Premium </span>
                            <p class="text-muted" style="font-size: 13px">These customizations are optional. Default values will be used if nothing is provided:</p>
                            <ul>
                                <% foreach (var customization in app.customization)
                                    {  %>
                                <li><%= customization %></li>
                                <% }  %>
                            </ul>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>


            <% } %>
        </div>
        <!--end row-->









        <%--Generate Modal windows--%>
        <!-- Modal -->
        <div id="modalAppLimitReached" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 id="appLimitTitle" class="modal-title">App Limit Reached</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>

                    </div>
                    <div class="modal-body">

                        <h6 style="font-weight: 800">Please upgrade:</h6>
                        <p class="text-muted" style="font-size: 14px">Your current membership has a maximum of 3 apps enabled at the same time. To enable more simultaneously, consider upgrading.</p>
                        <p class="text-muted" style="font-size: 14px">You can always disable apps you're not using so you can enable others 😊</p>
                    </div>
                    <div class="modal-footer">
                        <a href="/Plan" type="button" class="btn btn-primary">Upgrade</a>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>


    </form>





    <script>

        $(document).ready(function () {
            var currentURL = window.location.href;
            if (currentURL.endsWith("Dashboard")) {
                $("#widgetDashboard").addClass("active");
            } else if (currentURL.endsWith("Plan")) {
                $("#widgetPlan").addClass("active");
            } else if (currentURL.endsWith("Settings")) {
                $("#widgetSettings").addClass("active");
            }
        });

        function showConfigModal(appName, appID, enabled) {

            $('#modal' + appID).modal('show');
        }
    </script>
</asp:Content>
