﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.0
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PruebaConsola.localhost {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1", ItemName="string")]
    [System.SerializableAttribute()]
    public class ArrayOfString : System.Collections.Generic.List<string> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1", ConfigurationName="localhost.WebService1Soap")]
    public interface WebService1Soap {
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento HelloWorldResult del espacio de nombres http://tempuri.org/TestFacturaElectronicaWeb/WebService1 no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/TestFacturaElectronicaWeb/WebService1/HelloWorld", ReplyAction="*")]
        PruebaConsola.localhost.HelloWorldResponse HelloWorld(PruebaConsola.localhost.HelloWorldRequest request);
        
        // CODEGEN: Se está generando un contrato de mensaje, ya que el nombre de elemento rutaCertificado del espacio de nombres http://tempuri.org/TestFacturaElectronicaWeb/WebService1 no está marcado para aceptar valores nil.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/TestFacturaElectronicaWeb/WebService1/AutorizarConWSAA", ReplyAction="*")]
        PruebaConsola.localhost.AutorizarConWSAAResponse AutorizarConWSAA(PruebaConsola.localhost.AutorizarConWSAARequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1", Order=0)]
        public PruebaConsola.localhost.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(PruebaConsola.localhost.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1", Order=0)]
        public PruebaConsola.localhost.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(PruebaConsola.localhost.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AutorizarConWSAARequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AutorizarConWSAA", Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1", Order=0)]
        public PruebaConsola.localhost.AutorizarConWSAARequestBody Body;
        
        public AutorizarConWSAARequest() {
        }
        
        public AutorizarConWSAARequest(PruebaConsola.localhost.AutorizarConWSAARequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1")]
    public partial class AutorizarConWSAARequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string rutaCertificado;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public long cuit;
        
        public AutorizarConWSAARequestBody() {
        }
        
        public AutorizarConWSAARequestBody(string rutaCertificado, long cuit) {
            this.rutaCertificado = rutaCertificado;
            this.cuit = cuit;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AutorizarConWSAAResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AutorizarConWSAAResponse", Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1", Order=0)]
        public PruebaConsola.localhost.AutorizarConWSAAResponseBody Body;
        
        public AutorizarConWSAAResponse() {
        }
        
        public AutorizarConWSAAResponse(PruebaConsola.localhost.AutorizarConWSAAResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/TestFacturaElectronicaWeb/WebService1")]
    public partial class AutorizarConWSAAResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public PruebaConsola.localhost.ArrayOfString AutorizarConWSAAResult;
        
        public AutorizarConWSAAResponseBody() {
        }
        
        public AutorizarConWSAAResponseBody(PruebaConsola.localhost.ArrayOfString AutorizarConWSAAResult) {
            this.AutorizarConWSAAResult = AutorizarConWSAAResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebService1SoapChannel : PruebaConsola.localhost.WebService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebService1SoapClient : System.ServiceModel.ClientBase<PruebaConsola.localhost.WebService1Soap>, PruebaConsola.localhost.WebService1Soap {
        
        public WebService1SoapClient() {
        }
        
        public WebService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PruebaConsola.localhost.HelloWorldResponse PruebaConsola.localhost.WebService1Soap.HelloWorld(PruebaConsola.localhost.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            PruebaConsola.localhost.HelloWorldRequest inValue = new PruebaConsola.localhost.HelloWorldRequest();
            inValue.Body = new PruebaConsola.localhost.HelloWorldRequestBody();
            PruebaConsola.localhost.HelloWorldResponse retVal = ((PruebaConsola.localhost.WebService1Soap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        PruebaConsola.localhost.AutorizarConWSAAResponse PruebaConsola.localhost.WebService1Soap.AutorizarConWSAA(PruebaConsola.localhost.AutorizarConWSAARequest request) {
            return base.Channel.AutorizarConWSAA(request);
        }
        
        public PruebaConsola.localhost.ArrayOfString AutorizarConWSAA(string rutaCertificado, long cuit) {
            PruebaConsola.localhost.AutorizarConWSAARequest inValue = new PruebaConsola.localhost.AutorizarConWSAARequest();
            inValue.Body = new PruebaConsola.localhost.AutorizarConWSAARequestBody();
            inValue.Body.rutaCertificado = rutaCertificado;
            inValue.Body.cuit = cuit;
            PruebaConsola.localhost.AutorizarConWSAAResponse retVal = ((PruebaConsola.localhost.WebService1Soap)(this)).AutorizarConWSAA(inValue);
            return retVal.Body.AutorizarConWSAAResult;
        }
    }
}
