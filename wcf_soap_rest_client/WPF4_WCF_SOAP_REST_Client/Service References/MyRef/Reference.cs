﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPF4_WCF_SOAP_REST_Client.MyRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Employee", Namespace="http://schemas.datacontract.org/2004/07/WCF_SOAP_REST_Service")]
    [System.SerializableAttribute()]
    public partial class Employee : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeptNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmpNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int EmpNoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DeptName {
            get {
                return this.DeptNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DeptNameField, value) != true)) {
                    this.DeptNameField = value;
                    this.RaisePropertyChanged("DeptName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmpName {
            get {
                return this.EmpNameField;
            }
            set {
                if ((object.ReferenceEquals(this.EmpNameField, value) != true)) {
                    this.EmpNameField = value;
                    this.RaisePropertyChanged("EmpName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int EmpNo {
            get {
                return this.EmpNoField;
            }
            set {
                if ((this.EmpNoField.Equals(value) != true)) {
                    this.EmpNoField = value;
                    this.RaisePropertyChanged("EmpNo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyRef.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetEmployees", ReplyAction="http://tempuri.org/IService/GetEmployeesResponse")]
        WPF4_WCF_SOAP_REST_Client.MyRef.Employee[] GetEmployees();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : WPF4_WCF_SOAP_REST_Client.MyRef.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<WPF4_WCF_SOAP_REST_Client.MyRef.IService>, WPF4_WCF_SOAP_REST_Client.MyRef.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WPF4_WCF_SOAP_REST_Client.MyRef.Employee[] GetEmployees() {
            return base.Channel.GetEmployees();
        }
    }
}
