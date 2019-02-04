![](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Media AI
</div>

<div class="MCWHeader2">
Hands-on lab unguided
</div>

<div class="MCWHeader3">
January 2019
</div>


Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places, and events depicted herein are fictitious, and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience, and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2018 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

<!-- TOC -->

- [Media AI hands-on lab unguided](#media-ai-hands-on-lab-unguided)
    - [Abstract and learning objectives](#abstract-and-learning-objectives)
    - [Overview](#overview)
    - [Solution architecture](#solution-architecture)
    - [Requirements](#requirements)
    - [Exercise 1: Signup for Video Indexer API Service](#exercise-1-signup-for-video-indexer-api-service)
        - [Task 1: Signup for Video Indexer](#task-1-signup-for-video-indexer)
            - [Tasks to complete](#tasks-to-complete)
            - [Exit criteria](#exit-criteria)
        - [Task 2: Copy Video Indexer API Key](#task-2-copy-video-indexer-api-key)
            - [Tasks to complete](#tasks-to-complete-1)
            - [Exit criteria](#exit-criteria-1)
        - [Task 3: Copy Video Indexer Account ID](#task-3-copy-video-indexer-account-id)
            - [Tasks to complete](#tasks-to-complete-2)
            - [Exit criteria](#exit-criteria-2)
    - [Exercise 2: Setup video import workflow](#exercise-2-setup-video-import-workflow)
        - [Task 1: Create storage account for video files](#task-1-create-storage-account-for-video-files)
            - [Tasks to complete](#tasks-to-complete-3)
            - [Exit criteria](#exit-criteria-3)
        - [Task 2: Create Azure Logic App to process videos](#task-2-create-azure-logic-app-to-process-videos)
            - [Tasks to complete](#tasks-to-complete-4)
            - [Exit criteria](#exit-criteria-4)
    - [Exercise 3: Enable admin website to upload videos](#exercise-3-enable-admin-website-to-upload-videos)
        - [Task 1: Provision Cosmos DB account](#task-1-provision-cosmos-db-account)
            - [Tasks to complete](#tasks-to-complete-5)
            - [Exit criteria](#exit-criteria-5)
        - [Task 2: Integrate Cosmos DB into admin website](#task-2-integrate-cosmos-db-into-admin-website)
            - [Tasks to complete](#tasks-to-complete-6)
            - [Exit criteria](#exit-criteria-6)
        - [Task 3: Integrate File Upload into Admin Web App](#task-3-integrate-file-upload-into-admin-web-app)
            - [Tasks to complete](#tasks-to-complete-7)
            - [Exit criteria](#exit-criteria-7)
        - [Task 4: Add ability to delete video](#task-4-add-ability-to-delete-video)
            - [Tasks to complete](#tasks-to-complete-8)
            - [Exit criteria](#exit-criteria-8)
        - [Task 5: Deploy admin website to an Azure Web App](#task-5-deploy-admin-website-to-an-azure-web-app)
            - [Tasks to complete](#tasks-to-complete-9)
            - [Exit criteria](#exit-criteria-9)
        - [Task 6: Configure application settings](#task-6-configure-application-settings)
            - [Tasks to complete](#tasks-to-complete-10)
            - [Exit criteria](#exit-criteria-10)
    - [Exercise 4: Update video status when processing is complete](#exercise-4-update-video-status-when-processing-is-complete)
        - [Task 1: Create Azure Function](#task-1-create-azure-function)
            - [Tasks to complete](#tasks-to-complete-11)
            - [Exit criteria](#exit-criteria-11)
        - [Task 2: Update Cosmos DB document with video processing state](#task-2-update-cosmos-db-document-with-video-processing-state)
            - [Tasks to complete](#tasks-to-complete-12)
            - [Exit criteria](#exit-criteria-12)
        - [Task 3: Update video state when processing is complete](#task-3-update-video-state-when-processing-is-complete)
            - [Tasks to complete](#tasks-to-complete-13)
            - [Exit criteria](#exit-criteria-13)
    - [Exercise 5: Add video player to front-end application](#exercise-5-add-video-player-to-front-end-application)
        - [Task 1: Integrate Cosmos DB into front-end application](#task-1-integrate-cosmos-db-into-front-end-application)
            - [Tasks to complete](#tasks-to-complete-14)
            - [Exit criteria](#exit-criteria-14)
        - [Task 2: Display video thumbnail image](#task-2-display-video-thumbnail-image)
            - [Tasks to complete](#tasks-to-complete-15)
            - [Exit criteria](#exit-criteria-15)
        - [Task 3: Add video player](#task-3-add-video-player)
            - [Tasks to complete](#tasks-to-complete-16)
            - [Exit criteria](#exit-criteria-16)
        - [Task 4: Add video insights](#task-4-add-video-insights)
            - [Tasks to complete](#tasks-to-complete-17)
            - [Exit criteria](#exit-criteria-17)
        - [Task 5: Integrate video player and insights together](#task-5-integrate-video-player-and-insights-together)
            - [Tasks to complete](#tasks-to-complete-18)
            - [Exit criteria](#exit-criteria-18)
        - [Task 6: Deploy public website to an Azure Web App](#task-6-deploy-public-website-to-an-azure-web-app)
            - [Tasks to complete](#tasks-to-complete-19)
            - [Exit criteria](#exit-criteria-19)
        - [Task 7: Configure application settings](#task-7-configure-application-settings)
            - [Tasks to complete](#tasks-to-complete-20)
            - [Exit criteria](#exit-criteria-20)
    - [Exercise 6: Test the application](#exercise-6-test-the-application)
        - [Task 1: Upload video to admin website](#task-1-upload-video-to-admin-website)
            - [Tasks to complete](#tasks-to-complete-21)
            - [Exit criteria](#exit-criteria-21)
        - [Task 2: View video and insights in public website](#task-2-view-video-and-insights-in-public-website)
            - [Tasks to complete](#tasks-to-complete-22)
            - [Exit criteria](#exit-criteria-22)
    - [After the hands-on lab](#after-the-hands-on-lab)
        - [Task 1: Delete resources](#task-1-delete-resources)

<!-- /TOC -->

# Media AI hands-on lab unguided

## Abstract and learning objectives 

In this hands-on lab, you will build, setup, and configure a Web Application that performs media streaming using Azure Services; including the Video Indexer API. You will also learn how to implement video processing using Logic Apps, Azure Functions, and Video Indexer API to encode and transcribe videos.

By the end of this hands-on lab you will be better able to build and manages media applications including the setup the Video Indexer API, upload videos to Blob Storage to be encoded with Azure Video Indexer, and integrate Video Indexer through Logic Apps and Azure Functions.

## Overview

Contoso has asked you to build a media streaming service so they can deliver their on-demand video training courses to their customers. For this solution, Contoso wants to use PaaS and Serverless services within the Microsoft Azure platform to help minimize development time and increase ease of maintenance. They would also like you to integrate automatic transcript generation and caption translation within the video encoding process so overall video production cost can be reduced, as well as improving the video player experience for their customers across the globe.

## Solution architecture

![Icons that are connected by arrows comprise this rectangular diagram. From the top left, Web App -- Admin Site points linearly down to Azure Storage, which points right to a blue box labeled Azure Logic App. In this box, Azure Logic App points at Video Indexer Connector, and the blue box points right to Video Indexer Service. The rest of the icons use double-sided arrows and point to and from: Video Indexer Service up to Web App -- Frontend, left to Database, which points down through the middle of the rectangle to Azure Function and back down to the blue Azure Logic App box. A double-sided arrow also connects Azure Function to Video Indexer Service, and on the right side, a double-sided arrow points from Web App -- Frontend to Media Player.](images/Hands-onlabunguided-MediaAIimages/media/image2.png "Solution architecture diagram")

## Requirements

-   Microsoft Azure subscription

-   Local machine or Azure LABVM virtual machine configured with:

    -   Visual Studio 2017 Community Edition or later.


## Exercise 1: Signup for Video Indexer API Service

Duration: 15 minutes

In this exercise, you will setup the Video Indexer API within Microsoft Azure.

### Task 1: Signup for Video Indexer

#### Tasks to complete

1.  Signup for an account for the **Video Indexer API**: <https://api-portal.videoindexer.ai/>.

2.  Subscribe to the **Authorization** API Product Subscription.

#### Exit criteria

-   You have an Account and Subscription to the **Authorization** API Product within the **Video Indexer API**.

### Task 2: Copy Video Indexer API Key

#### Tasks to complete

1.  Locate the **API Key** for your **Video Indexer API Authorization Subscription**.

#### Exit criteria

-   You've copied the **Video Indexer API Key** from your Subscription for later use.

### Task 3: Copy Video Indexer Account ID

#### Tasks to complete

1.  Locate the **Account Key** for your **Video Indexer** account.

#### Exit criteria

- You've copied the **Video Indexer Account ID** from your Video Indexer Account at <https://videoindexer.ai>.

##  Exercise 2: Setup video import workflow

Duration: 20 minutes

In this exercise, you will set the import workflow for uploading and importing videos using the Video Indexer API.

### Task 1: Create storage account for video files

#### Tasks to complete

1.  Create a storage account that can be used to upload video files to.

    a.  The storage account should have a Blob Container named **videos**.

#### Exit criteria

-   You have a storage account to use for uploading videos within the application.

### Task 2: Create Azure Logic App to process videos

#### Tasks to complete

1.  Create a new Azure Logic App that is triggered when a new Blob is saved in the storage account.

2.  The Logic App integrates the Video Indexer Connector to upload and index the uploaded video.

#### Exit criteria

-   A Logic App has been created that is triggered by a new Blob in the Storage Account, then passes that video file to the Video Indexer Connector for processing.

## Exercise 3: Enable admin website to upload videos

Duration: 45 minutes

In this exercise, you will wire up the Admin website to enable Video Upload functionality.

### Task 1: Provision Cosmos DB account

#### Tasks to complete

1.  Provision an Azure Cosmos DB Account that will be used as the backend database for the application.

#### Exit criteria

-   The Azure Cosmos DB Account should be provisioned using the **SQL API**, plus have a database named **learning**, a collection named **videos**.

### Task 2: Integrate Cosmos DB into admin website

#### Tasks to complete

1.  Add code to the **ContosoLearning.Data** and **ContosoLearning.Web.Admin** projects so that the Data Access Layer (DAL) is coded to work with using the Azure Cosmos DB account as the backend database for the application.

#### Exit criteria

-   The **Get**, **GetAll**, **Insert**, and **Delete** data access methods have code that integrates with Azure Cosmos DB.

### Task 3: Integrate File Upload into Admin Web App

#### Tasks to complete

1.  Add file upload capabilities to the admin website application so admin users can upload new videos.

#### Exit criteria

-   Videos can be uploaded through the admin website and saved to Azure Storage.

### Task 4: Add ability to delete video

#### Tasks to complete

1.  Modify the **ContosoLearning.Web.Admin** website application so that the **Delete** code for deleting videos is completed.

#### Exit criteria

-   Admin users can delete videos that have been previously uploaded.

-   When a video is deleted, it also deletes the video from **Video Indexer** as well as **Cosmos DB** and the **Storage Account**.

### Task 5: Deploy admin website to an Azure Web App

#### Tasks to complete

1.  Deploy the **Admin** website application to be hosted within an Azure Web App.

#### Exit criteria

-   The **Admin** website is hosted in an Azure Web App.

### Task 6: Configure application settings

#### Tasks to complete

1.  Configure the **Application Settings** for the Azure Web App that's hosting the **Admin** website application.

#### Exit criteria

-   The admin website's application settings have been configured.

## Exercise 4: Update video status when processing is complete

Duration: 20 minutes

In this exercise, you will integrate an Azure Function with the Logic App Workflow so that the Azure Cosmos DB database is updated when a video is finished being processed within Video Indexer.

### Task 1: Create Azure Function

#### Tasks to complete

1.  Create an Azure Function that can be called by the Azure Logic App.

#### Exit criteria

-   A new Azure Function was created.

    -   It accepts **"documentId"** and **"videoId"** parameters so it can integrate with Cosmos DB and Video Indexer.

### Task 2: Update Cosmos DB document with video processing state

#### Tasks to complete

1.  Code the Azure Function so it can update the Cosmos DB document for the video with the current **Processing State** for the video within Azure Video Indexer.

#### Exit criteria

-   The Azure Function has an **Input binding,** so it can read and write the document for the **documentId** passed to the function from the Logic App.

-   The document is updated to contain the **videoId** property.

### Task 3: Update video state when processing is complete

#### Tasks to complete

1.  Update the Azure Logic App to integrate the Azure Function that was created.

#### Exit criteria

-   The Azure Logic App calls the Azure Function (by passing **documentId** and **videoId**) periodically while the video is processing to update status, as well as calling it again when processing is completed.

## Exercise 5: Add video player to front-end application

Duration: 30 minutes

In this exercise, you will extend the front-end application foundation to include a video player and Cognitive Services insights for the videos.

### Task 1: Integrate Cosmos DB into front-end application

#### Tasks to complete

1.  Update the **ContosoLearning.Web.Public** application project to include code that integrates the Azure Cosmos DB collection as the database backend.

#### Exit criteria

-   The **Index** list page displays a list of all videos in the database.

-   The **Video** detail pages or views of the application are coded to load data for the video matching the document ID passed in.

### Task 2: Display video thumbnail image

#### Tasks to complete

1.  Add display of the video thumbnail images to the **Index** list page by populating the **ThumbnailUrl** property by calling the **Video Indexer API**.

#### Exit criteria

-   When the **Index** list page is displayed, the thumbnail images for videos in the database are shown in the UI.

### Task 3: Add video player

#### Tasks to complete

1.  Add the video player from Video Indexer to the **Video** view or page.

#### Exit criteria

-   The **Video** view or page in the application shows the video player for the video being shown.

### Task 4: Add video insights

#### Tasks to complete

1.  Add the video insights from Video Indexer to the **Video** view or page.

#### Exit criteria

-   The **Video** view or page in the application shows the video insights from Video Indexer next to the video player.

### Task 5: Integrate video player and insights together

#### Tasks to complete

1.  Integrate the video player and video insights from video insights.

#### Exit criteria

-   The video player and video insights UI within the **Video** page or view in the application interact when the video is playing so the video insights enhance the video player experience.

### Task 6: Deploy public website to an Azure Web App

#### Tasks to complete

1.  Deploy the **Public** website application to be hosted within an Azure Web App.

#### Exit criteria

-   The **Public** website is hosted in an Azure Web App.

### Task 7: Configure application settings

#### Tasks to complete

1.  Configure the **Application Settings** for the Azure Web App that's hosting the **Public** website application.

#### Exit criteria

-   The public website's application settings have been configured.

## Exercise 6: Test the application

Duration: 15 minutes

In this exercise, you will test out the Admin and Public web applications.

### Task 1: Upload video to admin website

#### Tasks to complete

1.  Use the **Admin** website to upload at least one video.

#### Exit criteria

-   Videos can be uploaded through the **Admin** website.

### Task 2: View video and insights in public website

#### Tasks to complete

1.  Access the **Public** website to play videos and view their video insights.

#### Exit criteria

-   The **Public** website displays a list of videos in the database along with thumbnail images for each video.

-   The **Public** website allows users to view and play videos along with the ability to view and interact with the video insights; such as video transcripts and captions translated into multiple different languages.

## After the hands-on lab 

Duration: 10 minutes

### Task 1: Delete resources

1.  Now that the hands-on lab is complete, go ahead and delete all the Resource Groups that were created for this lab. You will no longer need those resources and it will be beneficial to clean up your Azure Subscription.

You should follow all steps provided *after* attending the Hands-on lab.