<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Empleado_Modificar.aspx.cs" Inherits="FrameWork.Empleado.Empleado_Modificar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">





    <asp:Label ID="lblTitulo" runat="server" CssClass="fs-4 fw-bold"></asp:Label>

    <div class="mb-3">
        <label class="form-label">DPI</label>
        <asp:TextBox ID="txtDPI" runat="server" CssClass="form-control" BackColor="#FF3300"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label class="form-label">Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label class="form-label">Apellido</label>
        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
    </div>

     <div class="mb-3">
     <label class="form-label">Sexo</label>
     <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select"></asp:DropDownList>
 </div>
     <div class="mb-3">
     <label class="form-label">Ingreso</label>
     <asp:TextBox ID="txtDateIngreso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
 </div>

     <div class="mb-3">
     <label class="form-label">Nacimiento</label>
    <asp:TextBox ID="txtDateNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
 </div>

     <div class="mb-3">
     <label class="form-label">Edad</label>
     <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control" ReadOnly></asp:TextBox>
 </div>

     <div class="mb-3">
     <label class="form-label">Direccion</label>
     <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
 </div>

     <div class="mb-3">
     <label class="form-label">Nit</label>
     <asp:TextBox ID="txtNit" runat="server" CssClass="form-control"></asp:TextBox>
 </div>

     <div class="mb-3">
     <label class="form-label">Departamento</label>
     <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-select"></asp:DropDownList>
 </div>

    <div class="mb-3">
        <label class="form-label">Estado</label>
        <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" ReadOnly BackColor="#FF3300"></asp:TextBox>
    </div>

    <asp:Button ID="btnSubmit" runat="server" Text="Enviar" CssClass="btn btn-sm btn-primary" OnClick="btnSubmit_Click" />
    <asp:LinkButton runat="server" PostBackUrl="~/Empleado/Empleado_Listado.aspx" CssClass="btn btn-sm btn-warning">Volver</asp:LinkButton>




</asp:Content>
