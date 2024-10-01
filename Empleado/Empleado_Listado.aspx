<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleado_Listado.aspx.cs" Inherits="FrameWork.Empleado.Empleado_Listado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div class="row">
        <div class="col-12">

            <asp:Button runat="server" OnClick="Nuevo_Click" CssClass="btn btn-sm btn-success" Text="Nuevo" />
        </div>
    </div>

    <div class="row">
        <div class="col-12">
               
            <asp:GridView ID="GVEmpleado" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">

                 <Columns>
                    <asp:BoundField DataField="DPI" HeaderText="Id" />
                    <asp:BoundField DataField="Nombres" HeaderText="Departamento" />
                    <asp:BoundField DataField="Apellidos" HeaderText="Estado" />
                     <asp:BoundField DataField="Sexo" HeaderText="Sexo" />
                     <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
     
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("DPI") %>'
                                OnClick="Editar_Click" CssClass="btn btn-sm btn-primary"
                                > Editar</asp:LinkButton>

                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("DPI") %>'
                                OnClick="Eliminar_Click" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Desea eliminar?')"
                                > Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>




</asp:Content>
