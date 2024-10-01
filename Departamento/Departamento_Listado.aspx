<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departamento_Listado.aspx.cs" Inherits="FrameWork.Departamento.Departamento"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-12">

            <asp:Button runat="server" OnClick="Nuevo_Click" CssClass="btn btn-sm btn-success" Text="Nuevo" />
        </div>
    </div>

    <div class="row">
        <div class="col-12">
               
            <asp:GridView ID="GVDepartamento" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">

                 <Columns>
                    <asp:BoundField DataField="DepartamentoId" HeaderText="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Departamento" />
                    <asp:BoundField DataField="Estado" HeaderText="Estado" />
     
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("DepartamentoId") %>'
                                OnClick="Editar_Click" CssClass="btn btn-sm btn-primary"
                                > Editar</asp:LinkButton>

                            <asp:LinkButton runat="server" CommandArgument='<%# Eval("DepartamentoId") %>'
                                OnClick="Eliminar_Click" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Desea eliminar?')"
                                > Eliminar</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>


</asp:Content>
