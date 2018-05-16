# Media AI

## Hands-on lab unguided

## March 2018

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

# Contents

[Media AI hands-on lab unguided](#_Toc492640499)

[Abstract and learning objectives](#abstract-and-learning-objectives)

[Overview](#overview)

[Solution architecture](#solution-architecture)

[Requirements](#requirements)

[Before the hands-on lab](#before-the-hands-on-lab)

[Prerequisites](#prerequisites)

[Task 1: Configure a development environment](#task-1-configure-a-development-environment)

[Task 2: Disable IE enhanced security](#task-2-disable-ie-enhanced-security)

[Task 3: Update Visual Studio Tools for Azure Functions](#task-3-update-visual-studio-tools-for-azure-functions)

[Task 4: Install Visual Studio Tools for Logic Apps](#task-4-install-visual-studio-tools-for-logic-apps)

[Task 5: Validate connectivity to Azure](#task-5-validate-connectivity-to-azure)

[Task 6: Download the exercise files](#task-6-download-the-exercise-files)

[Exercise 1: Signup for Video Indexer API Service](#exercise-1-signup-for-video-indexer-api-service)

[Task 1: Signup for Video Indexer](#task-1-signup-for-video-indexer)

[Task 2: Copy Video Indexer API Key](#task-2-copy-video-indexer-api-key)

[Exercise 2: Setup video import workflow](#exercise-2-setup-video-import-workflow)

[Task 1: Create Storage Account for video files](#task-1-create-storage-account-for-video-files)

[Task 2: Create Azure Logic App to process videos](#task-2-create-azure-logic-app-to-process-videos)

[Exercise 3: Enable admin website to upload videos](#exercise-3-enable-admin-website-to-upload-videos)

[Task 1: Provision Cosmos DB Account](#task-1-provision-cosmos-db-account)

[Task 2: Integrate Cosmos DB into Admin Website](#task-2-integrate-cosmos-db-into-admin-website)

[Task 3: Integrate File Upload into Admin Web App](#task-3-integrate-file-upload-into-admin-web-app)

[Task 4: Add ability to delete video](#task-4-add-ability-to-delete-video)

[Task 5: Deploy Admin website to an Azure Web App](#task-5-deploy-admin-website-to-an-azure-web-app)

[Task 6: Configure Application Settings](#task-6-configure-application-settings)

[Exercise 4: Update video status when processing is complete](#exercise-4-update-video-status-when-processing-is-complete)

[Task 1: Create Azure Function](#task-1-create-azure-function)

[Task 2: Update Cosmos DB Document with Video Processing State](#task-2-update-cosmos-db-document-with-video-processing-state)

[Task 3: Update Video State when processing is complete](#task-3-update-video-state-when-processing-is-complete)

[Exercise 5: Add Video Player to Front-End Application](#exercise-5-add-video-player-to-front-end-application)

[Task 1: Integrate Cosmos DB into Front-End Application](#task-1-integrate-cosmos-db-into-front-end-application)

[Task 2: Display Video Thumbnail Image](#task-2-display-video-thumbnail-image)

[Task 3: Add Video Player](#task-3-add-video-player)

[Task 4: Add Video Insights](#task-4-add-video-insights)

[Task 5: Integrate Video Player and Insights together](#task-5-integrate-video-player-and-insights-together)

[Task 6: Deploy Public website to an Azure Web App](#task-6-deploy-public-website-to-an-azure-web-app)

[Task 7: Configure Application Settings](#task-7-configure-application-settings)

[Exercise 6: Test the application](#exercise-6-test-the-application)

[Task 1: Upload Video to Admin website](#task-1-upload-video-to-admin-website)

[Task 2: View Video and Insights in Public website](#task-2-view-video-and-insights-in-public-website)

[After the hands-on lab](#after-the-hands-on-lab)

[Task 1: Delete resources](#task-1-delete-resources)

## Media AI hands-on lab unguided

## Abstract and learning objectives 

In this workshop, students will learn how to build, setup, and configure a Web Application that performs media streaming using Azure Services; including the Video Indexer API. Students will also learn how to implement video processing using Logic Apps, Azure Functions, and Video Indexer API to encode and transcribe videos.

Students will be better able to build media applications including:

-   Setup Video Indexer API

-   Upload videos to Blob Storage to be encoded with Azure Video Indexer

-   Integrate Video Indexer through Logic Apps and Azure Functions

## Overview

Contoso has asked you to build a media streaming service so they can deliver their on-demand video training courses to their customers. For this solution, Contoso wants to use PaaS and Serverless services within the Microsoft Azure platform to help minimize development time and increase ease of maintenance. They would also like you to integrate automatic transcript generation and caption translation within the video encoding process so overall video production cost can be reduced, as well as improving the video player experience for their customers across the globe.

## Solution architecture

![Icons that are connected by arrows comprise this rectangular diagram. From the top left, Web App -- Admin Site points linearly down to Azure Storage, which points right to a blue box labeled Azure Logic App. In this box, Azure Logic App points at Video Indexer Connector, and the blue box points right to Video Indexer Service. The rest of the icons use double-sided arrows and point to and from: Video Indexer Service up to Web App -- Frontend, left to Database, which points down through the middle of the rectangle to Azure Function and back down to the blue Azure Logic App box. A double-sided arrow also connects Azure Function to Video Indexer Service, and on the right side, a double-sided arrow points from Web App -- Frontend to Media Player.](images/Hands-onlabunguided-MediaAIimages/media/image2.png "Solution architecture diagram")

## Requirements

-   Microsoft Azure subscription

-   Local machine or Azure LABVM virtual machine configured with:

    -   Visual Studio 2017 Community Edition or later

## Before the hands-on lab

Duration: 30 minutes

In this lab, you will create a developer environment and download the required files for this course if you do not already have one that meets the requirements.

#### Prerequisites

1.  Microsoft Azure subscription <http://azure.microsoft.com/en-us/pricing/free-trial/>

2.  Client computer with Windows 7 or later with Visual Studio 2017

#### Task 1: Configure a development environment

If you do not have a machine setup with Visual Studio 2017 Community complete this task.

1.  Create a virtual machine in Azure using the **Visual Studio Community 2017 (latest release) on Windows Server 2016 (x64)** image. This is important as you need to have Visual Studio 2017 version 15.4 or later to complete this lab.

    ![In this screenshot, visual studio community 2017 is entered in a search box, and the Visual Studio Community 2017 (latest release) on Windows Server 2016 (x64) row is selected and highlighted in the search results.](images/Hands-onlabunguided-MediaAIimages/media/image3.png "Use the image above to create a virtual machine in Azure")

It is **highly** recommended to use a DS2 or D2 instance size for this VM.

#### Task 2: Disable IE enhanced security

Note: Sometimes this image has IE ESC disabled, and sometimes it does not.

1.  On the new VM, you just created click the Server Manager icon

    ![Server Manager is highlighted on the taskbar.](images/Hands-onlabunguided-MediaAIimages/media/image4.png "Select Server Manager")

2.  Click Local Server

    ![In Server Manager, Local Server is selected and highlighted in the navigation pane.](images/Hands-onlabunguided-MediaAIimages/media/image5.png "Select Local Server")

3.  On the right side of the pane, click **On** by IE Enhanced Security Configuration

    ![On is highlighted next to IE Enhanced Security Configuration.](images/Hands-onlabunguided-MediaAIimages/media/image6.png "Turn IE Enhanced Security Configuration on")

4.  Change to **Off** for Administrators and click **OK**

    ![Off is selected and highlighted under Administrators in the IE Enhanced Security Configuration dialog box, and OK is highlighted at the bottom.](images/Hands-onlabunguided-MediaAIimages/media/image7.png "Turn Administrators off")

#### Task 3: Update Visual Studio Tools for Azure Functions

1.  Open Visual Studio 2017, then click on the **Tools** menu, then click on **Extensions and Updates...\
    **![In Visual Studio 2017, the Tools menu is highlighted and labeled 1, and Extensions and Updates is selected, highlighted, and labeled 2 in the submenu.](images/Hands-onlabunguided-MediaAIimages/media/image8.png "Select Extensions and Updates")

2.  On the Extensions and Updates dialog, click on **Updates**, then **Visual Studio Marketplace** on the left side of the dialog, then click on **Update** for the **Azure Functions and Web Jobs Tools** extension to update to the latest version**\
    **![Updates (labeled 1) and Visual Studio Marketplace (labeled 2) are highlighted on the left side of the Extensions and Updates dialog box, and the Update button and the Azure Functions and Web Jobs Tools extension (labeled 3) are selected and highlighted on the right.](images/Hands-onlabunguided-MediaAIimages/media/image9.png "Update Azure Functions and Web Jobs Tools")

#### Task 4: Install Visual Studio Tools for Logic Apps

1.  Within the **Extensions and Updates** dialog within Visual Studio 2017 sill open from the previous task, click on the **Online** category on the left side of the dialog.\
    ![Online is highlighted on the left side of the Extensions and Updates dialog box.](images/Hands-onlabunguided-MediaAIimages/media/image10.png "Select the Online category")

2.  In the **Search** box in the upper right of the dialog, type in **Azure Logic Apps** to search for the **Azure Logic Apps Tools for Visual Studio** extension. Then click **Download** on the Extension to install it.\
    ![Search Results is highlighted on the left side of the Extensions and Updates dialog box, Azure Logic Apps is highlighted on the top right (labeled 1), and the Download button and the Azure Logic Apps Tools for Visual Studio extension (labeled 2) are selected and highlighted in the middle.](images/Hands-onlabunguided-MediaAIimages/media/image11.png "Download Azure Logic Apps Tools for Visual Studio")
    

3.  Click **Close** on the **Extensions and Updated** dialog![Search Results is highlighted on the left side of the Extensions and Updates dialog box, Azure Logic Apps Tools for Visual Studio is selected in the middle, and Close is highlighted on the bottom right.](images/Hands-onlabunguided-MediaAIimages/media/image12.png "Close the Extensions and Updated dialog box")

4.  Close Visual Studio, the pending installation of the Azure Logic Apps Tools for Visual Studio will automatically launch

5.  On the **VSIX Installer** dialog, click on **Modify**\
    ![Modify is highlighted at the bottom of the VSIX Installer dialog box.](images/Hands-onlabunguided-MediaAIimages/media/image13.png "Select Modify")

6.  Wait for the extension to be installed, this should only take about 1 minute\
    ![A progress bar is displayed in the VSIX Installer dialog box.](images/Hands-onlabunguided-MediaAIimages/media/image14.png "Wait for the extension to install")

7.  Once installation is complete, click **Close**\
    ![A Modifications Complete message is displayed in the VSIX Installer dialog box.](images/Hands-onlabunguided-MediaAIimages/media/image15.png "Close the installer")

8.  Restart Visual Studio now that the extension has been installed

#### Task 5: Validate connectivity to Azure

1.  From within the virtual machine, Launch Visual Studio 2017 and validate that you can login with your Microsoft Account when prompted

2.  Validate connectivity to your Azure subscription. Launch Visual Studio, open Server Explorer from the View menu, and ensure that you can connect to your Azure subscription

    ![An Azure subscription is highlighted in Server Explorer, and the submenu is displayed with an option to connect to your Azure subscription.](images/Hands-onlabunguided-MediaAIimages/media/image16.png "Validate connectivity")

#### Task 6: Download the exercise files 

1.  Download the exercise files for the training (from within the virtual machine)

    a.  Create a new folder on your computer named **C:\\Hackathon**

    b.  Download the support files (.zip format), <https://cloudworkshop.blob.core.windows.net/media-services-and-cdn/Media-Services-Student-Files.zip> to the new folder.

    c.  Extract the contents to the same folder

## Exercise 1: Signup for Video Indexer API Service

Duration: 15 minutes

In this exercise, you will setup the Video Indexer API within Microsoft Azure.

#### Task 1: Signup for Video Indexer

###### Tasks to complete

1.  Signup for an account for the **Video Indexer API**: <https://videobreakdown.portal.azure-api.net/>

2.  Subscribe to the **Free Preview** API Product Subscription

###### Exit criteria

-   You have an Account and Subscription to the **Free Preview** API Product within the **Video Indexer API**

#### Task 2: Copy Video Indexer API Key

###### Tasks to complete

1.  Locate the **API Key** for your **Video Indexer API Free Preview Subscription**

###### Exit criteria

-   You've copied the **Video Indexer API Key** from your Subscription for later use

##  Exercise 2: Setup video import workflow

Duration: 20 minutes

In this exercise, you will set the import workflow for uploading and importing videos using the Video Indexer API.

#### Task 1: Create Storage Account for video files

###### Tasks to complete

1.  Create a Storage Account that can be used to upload video files to

    a.  The Storage Account should have a Blob Container named **videos**

###### Exit criteria

-   You have a Storage Account to use for uploading videos within the Application

#### Task 2: Create Azure Logic App to process videos

###### Tasks to complete

1.  Create a new Azure Logic App that is triggered when a new Blob is saved in the Storage Account

2.  The Logic App integrates the Video Indexer Connector to upload and index the uploaded video

###### Exit criteria

-   A Logic App has been created that is triggered by a new Blob in the Storage Account, then passes that video file to the Video Indexer Connector for processing

## Exercise 3: Enable admin website to upload videos

Duration: 45 minutes

In this exercise, you will wire up the Admin website to enable Video Upload functionality.

#### Task 1: Provision Cosmos DB Account

###### Tasks to complete

1.  Provision an Azure Cosmos DB Account that will be used as the backend database for the application

###### Exit criteria

-   The Azure Cosmos DB Account should be provisioned using the **SQL API**, plus have a Database named **learning**, a Collection named **videos**

#### Task 2: Integrate Cosmos DB into Admin Website

###### Tasks to complete

1.  Add code to the **ContosoLearning.Data** and **ContosoLearning.Web.Admin** projects so that the Data Access Layer (DAL) is coded to work with using the Azure Cosmos DB Account as the backend database for the application

###### Exit criteria

-   The **Get**, **GetAll**, **Insert**, and **Delete** data access methods have code that integrates with Azure Cosmos DB

#### Task 3: Integrate File Upload into Admin Web App

###### Tasks to complete

1.  Add File Upload capabilities to the Admin Website application so admin users can upload new videos

###### Exit criteria

-   Videos can be uploaded through the Admin website and saved to Azure Storage

#### Task 4: Add ability to delete video

###### Tasks to complete

1.  Modify the **ContosoLearning.Web.Admin** website application so that the **Delete** code for deleting videos is completed

###### Exit criteria

-   Admin users can delete videos that have been previously uploaded

-   When a video is deleted, it also deletes the video from **Video Indexer** as well as **Cosmos DB** and the **Storage Account**

#### Task 5: Deploy Admin website to an Azure Web App

###### Tasks to complete

1.  Deploy the **Admin** website application to be hosted within an Azure Web App

###### Exit criteria

-   The **Admin** website is hosted in an Azure Web App

#### Task 6: Configure Application Settings

###### Tasks to complete

1.  Configure the **Application Settings** for the Azure Web App that's hosting the **Admin** website application

###### Exit criteria

-   The Admin website's Application Settings have been configured

## Exercise 4: Update video status when processing is complete

Duration: 20 minutes

In this exercise, you will integrate an Azure Function with the Logic App Workflow so that the Azure Cosmos DB database is updated when a video is finished being processed within Video Indexer.

#### Task 1: Create Azure Function

###### Tasks to complete

1.  Create an Azure Function that can be called by the Azure Logic App

###### Exit criteria

-   A new Azure Function was created.

    -   It accepts **"documentId"** and **"videoId"** parameters so it can integrate with Cosmos DB and Video Indexer

#### Task 2: Update Cosmos DB Document with Video Processing State

###### Tasks to complete

1.  Code the Azure Function so it can update the Cosmos DB Document for the Video with the current **Processing State** for the video within Azure Video Indexer

###### Exit criteria

-   The Azure Function has an **Input binding,** so it can read and write the Document for the **documentId** passed to the function from the Logic App

-   The document is updated to contain the **videoId** property

#### Task 3: Update Video State when processing is complete

###### Tasks to complete

1.  Update the Azure Logic App to integrate the Azure Function that was created

###### Exit criteria

-   The Azure Logic App calls the Azure Function (by passing **documentId** and **videoId**) periodically while the video is processing to update status, as well as calling it again when processing is completed

## Exercise 5: Add Video Player to Front-End Application

Duration: 30 minutes

In this exercise, you will extend the Front-End Application foundation to include a video player and Cognitive Services Insights for the Videos.

#### Task 1: Integrate Cosmos DB into Front-End Application

###### Tasks to complete

1.  Update the **ContosoLearning.Web.Public** application project to include code that integrates the Azure Cosmos DB Collection as the database backend

###### Exit criteria

-   The **Index** list page displays a list of all Videos in the database

-   The **Video** detail pages / views of the application are coded to load data for the Video matching the Document ID passed in

#### Task 2: Display Video Thumbnail Image

###### Tasks to complete

1.  Add display of the Video Thumbnail images to the **Index** list page by populating the **ThumbnailUrl** property by calling the **Video Indexer API**

###### Exit criteria

-   When the **Index** list page is displayed, the Thumbnail Images for Videos in the database are shown in the UI

#### Task 3: Add Video Player

###### Tasks to complete

1.  Add the Video Player from Video Indexer to the **Video** view / page

###### Exit criteria

-   The **Video** view / page in the application shows the video player for the Video being shown

#### Task 4: Add Video Insights

###### Tasks to complete

1.  Add the Video Insights from Video Indexer to the **Video** view / page

###### Exit criteria

-   The **Video** view / page in the application shows the Video Insights from Video Indexer next to the Video Player

#### Task 5: Integrate Video Player and Insights together

###### Tasks to complete

1.  Integrate the Video Player and Video Insights from Video Insights

###### Exit criteria

-   The Video Player and Video Insights UI within the **Video** page / view in the application interact when the video is playing so the Video Insights enhance the Video Player experience

#### Task 6: Deploy Public website to an Azure Web App

###### Tasks to complete

1.  Deploy the **Public** website application to be hosted within an Azure Web App

###### Exit criteria

-   The **Public** website is hosted in an Azure Web App

#### Task 7: Configure Application Settings

###### Tasks to complete

1.  Configure the **Application Settings** for the Azure Web App that's hosting the **Public** website application

###### Exit criteria

-   The Public website's Application Settings have been configured

## Exercise 6: Test the application

Duration: 15 minutes

In this exercise, you will test out the Admin and Public web applications.

#### Task 1: Upload Video to Admin website

###### Tasks to complete

1.  Use the **Admin** website to upload at least 1 video

###### Exit criteria

-   Videos can be uploaded through the **Admin** website

#### Task 2: View Video and Insights in Public website

###### Tasks to complete

1.  Access the **Public** website to play Videos and view their Video Insights

###### Exit criteria

-   The **Public** website displays a list of Videos in the database along with Thumbnail images for each video

-   The **Public** website allows users to view and play Videos along with the ability to view and interact with the Video Insights; such as video transcripts and captions translated into multiple different languages

####  

## After the hands-on lab 

Duration: 10 minutes

#### Task 1: Delete resources

1.  Now that the hands-on lab is complete, go ahead and delete all the Resource Groups that were created for this lab. You will no longer need those resources and it will be beneficial to clean up your Azure Subscription.

You should follow all steps provided *after* attending the Hands-on lab.
