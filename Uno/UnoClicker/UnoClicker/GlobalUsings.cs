﻿global using System;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Linq;
global using System.Net.Http;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.UI.Xaml;
global using Microsoft.UI.Xaml.Controls;
global using Microsoft.UI.Xaml.Media;
global using Microsoft.UI.Xaml.Navigation;
global using Refit;
global using Uno.Extensions;
global using Uno.Extensions.Configuration;
global using Uno.Extensions.Hosting;
global using Uno.Extensions.Http;
global using Uno.Extensions.Localization;
global using Uno.Extensions.Logging;
global using Uno.Extensions.Navigation;
global using UnoClicker.Business.Models;
global using UnoClicker.DataContracts;
global using UnoClicker.DataContracts.Serialization;
global using UnoClicker.Infrastructure;
global using UnoClicker.Presentation;
global using UnoClicker.Services.Caching;
global using UnoClicker.Services.Endpoints;
global using Windows.ApplicationModel;
global using Application = Microsoft.UI.Xaml.Application;
global using ApplicationExecutionState = Windows.ApplicationModel.Activation.ApplicationExecutionState;
