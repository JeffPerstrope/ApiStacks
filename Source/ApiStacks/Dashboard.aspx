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
                            <li class="list-inline-item"><a onclick="showConfigModal('<%= app.name %>', '<%= app.enabled %>');" class="rounded"><i data-feather="settings" class="fea icon-sm fea-social"></i></a></li>
                            <li class="list-inline-item"><a href="javascript:void(0)" class="rounded"><i data-feather="help-circle" class="fea icon-sm fea-social"></i></a></li>
                        </ul>
                        <!--end icon-->
                        <% if (app.enabled)
                            { %>
                        <a href="/Dashboard?deactivate=<%= app.id %>" id="<%= app.id %>" class="btn btn-light">Deactivate</a>
                        <%}
                            else
                            { %>
                        <a href="/Dashboard?activate=<%= app.id %>" id="<%= app.id %>" class="btn btn-primary">Activate</a>
                        <%} %>
                    </div>
                </div>
            </div>
            <!--end col-->
            <% } %>
        
    </div>
    <!--end row-->
    </form>


    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 id="modalAPIAppTitle"class="modal-title">Web Screenshots API</h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>

                </div>
                <div class="modal-body">
                    <div id="modalAPIAppAlert" class="alert" role="alert">API endpoint is currently active </div>

                    <h6>API keys:</h6>
                    <p>Private Key: <a class="text-primary">814f52b2-0b74-43cc-9848-eaeec97da3e0</a></p>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


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

        function showConfigModal(appName, enabled) {
            //Reset
            $("#modalAPIAppAlert").removeClass("alert-light");
            $("#modalAPIAppAlert").removeClass("alert-warning");

            $('#myModal').modal('show');
            $('#modalAPIAppTitle').text(appName)

            if (enabled == "True") {
                $("#modalAPIAppAlert").addClass("alert-light");
                $("#modalAPIAppAlert").text("API endpoint is currently active");
            } else {
                $("#modalAPIAppAlert").addClass("alert-warning");
                $("#modalAPIAppAlert").text("API endpoint is currently inactive. Calls to this endpoint will fail");
            }
        }
    </script>
</asp:Content>
