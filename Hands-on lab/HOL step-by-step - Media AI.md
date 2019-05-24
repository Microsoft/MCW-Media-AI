![](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Media AI
</div>

<div class="MCWHeader2">
Hands-on lab step-by-step
</div>

<div class="MCWHeader3">
May 2019
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places and events depicted herein are fictitious and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

© 2019 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**

<!-- TOC -->

- [Media AI hands-on lab step-by-step](#media-ai-hands-on-lab-step-by-step)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Overview](#overview)
  - [Solution architecture](#solution-architecture)
  - [Requirements](#requirements)
  - [Exercise 1: Signup for Video Indexer API service](#exercise-1-signup-for-video-indexer-api-service)
    - [Task 1: Signup for Video Indexer](#task-1-signup-for-video-indexer)
    - [Task 2: Copy Video Indexer API Key](#task-2-copy-video-indexer-api-key)
    - [Task 3: Copy Video Indexer Account ID](#task-3-copy-video-indexer-account-id)
  - [Exercise 2: Setup video import workflow](#exercise-2-setup-video-import-workflow)
    - [Task 1: Create storage account for video files](#task-1-create-storage-account-for-video-files)
    - [Task 2: Create Azure Logic App to process videos](#task-2-create-azure-logic-app-to-process-videos)
  - [Exercise 3: Enable admin website to upload videos](#exercise-3-enable-admin-website-to-upload-videos)
    - [Task 1: Provision Cosmos DB Account](#task-1-provision-cosmos-db-account)
    - [Task 2: Integrate Cosmos DB into admin web app](#task-2-integrate-cosmos-db-into-admin-web-app)
    - [Task 3: Integrate file upload into admin web app](#task-3-integrate-file-upload-into-admin-web-app)
    - [Task 4: Add ability to delete video](#task-4-add-ability-to-delete-video)
    - [Task 5: Deploy admin website to an Azure Web App](#task-5-deploy-admin-website-to-an-azure-web-app)
    - [Task 6: Configure application settings](#task-6-configure-application-settings)
  - [Exercise 4: Update video status when processing is complete](#exercise-4-update-video-status-when-processing-is-complete)
    - [Task 1: Create Azure Function](#task-1-create-azure-function)
    - [Task 2: Update Video State when processing is complete](#task-2-update-video-state-when-processing-is-complete)
  - [Exercise 5: Add video player to front-end application](#exercise-5-add-video-player-to-front-end-application)
    - [Task 1: Integrate Cosmos DB into front-end application](#task-1-integrate-cosmos-db-into-front-end-application)
    - [Task 2: Display video thumbnail image](#task-2-display-video-thumbnail-image)
    - [Task 3: Add video player](#task-3-add-video-player)
    - [Task 4: Add video insights](#task-4-add-video-insights)
    - [Task 5: Integrate video player and insights together](#task-5-integrate-video-player-and-insights-together)
    - [Task 6: Deploy public website to an Azure Web App](#task-6-deploy-public-website-to-an-azure-web-app)
    - [Task 7: Configure application settings](#task-7-configure-application-settings)
  - [Exercise 6: Test the application](#exercise-6-test-the-application)
    - [Task 1: Upload video to admin website](#task-1-upload-video-to-admin-website)
    - [Task 2: View video and insights in public website](#task-2-view-video-and-insights-in-public-website)
  - [After the hands-on lab](#after-the-hands-on-lab)
    - [Task 1: Delete resources](#task-1-delete-resources)

<!-- /TOC -->

# Media AI hands-on lab step-by-step

## Abstract and learning objectives

In this hands-on lab, you will build, setup and configure a Web Application that performs media streaming using Azure Services; including the Video Indexer API. You will also learn how to implement video processing using Logic Apps, Azure Functions and Video Indexer API to encode and transcribe videos.

At the end of this hands-on lab you will be better able to build and manage media applications including the setup the Video Indexer API, upload videos to Blob Storage to be encoded with Azure Video Indexer and integrate Video Indexer through Logic Apps and Azure Functions.

## Overview

Contoso has asked you to build a media streaming service so they can deliver their on-demand video training courses to their customers. For this solution, Contoso wants to use PaaS and Serverless services within the Microsoft Azure platform to help minimize development time and increase ease of maintenance. They would also like you to integrate automatic transcript generation and caption translation within the video encoding process so overall video production cost can be reduced, as well as improving the video player experience for their customers across the globe.

## Solution architecture

![Icons that are connected by arrows comprise this rectangular diagram. From the top left, Web App -- Admin Site points linearly down to Azure Storage, which points right to a blue box labeled Azure Logic App. In this box, Azure Logic App points at Video Indexer Connector and the blue box points right to Video Indexer Service. The rest of the icons use double-sided arrows and point to and from: Video Indexer Service up to Web App -- Frontend, left to Database, which points down through the middle of the rectangle to Azure Function and back down to the blue Azure Logic App box. A double-sided arrow also connects Azure Function to Video Indexer Service and on the right side, a double-sided arrow points from Web App -- Frontend to Media Player.](images/Hands-onlabstep-by-step-MediaAIimages/media/image2.png "Solution architecture diagram")

## Requirements

-   Microsoft Azure subscription: <http://azure.microsoft.com/en-us/pricing/free-trial/>

-   Local machine or Azure LABVM virtual machine configured with:

    -   Visual Studio 2017 Community Edition or later


## Exercise 1: Signup for Video Indexer API service

Duration: 15 minutes

In this exercise, you will setup the Video Indexer API within Microsoft Azure.

### Task 1: Signup for Video Indexer

1. Open a new browser window / tab and navigate to <https://api-portal.videoindexer.ai>.
   
   ```
   https://api-portal.videoindexer.ai
   ```
2. Select the **SIGN IN** link in the upper-right.

    ![SIGN IN is highlighted in the Video Indexer developer portal.](images/Hands-onlabstep-by-step-MediaAIimages/media/image17.png "Sign in to the Video Indexer developer portal")

3. Sign in with your **Microsoft** or **Azure AD** credentials. Use the same credentials you login to your Azure Subscription with.

    ![Azure Active Directory, Google and Microsoft are listed as sign-in options.](images/Hands-onlabstep-by-step-MediaAIimages/media/image18.png "Sign in with Microsoft or Azure AD credentials")

4. The first time you sign in, you will be prompted to authorize the **www.videoindexer.ai** application with permissions for your account.

    ![Permission authorization prompt is displayed for www.videoindexer.ai](images/Hands-onlabstep-by-step-MediaAIimages/media/image19.png "Authorize www.videoindexer.ai")

5. Once signed up and signed in, select on the **PRODUCTS** menu at the top.

    ![The PRODUCTS menu is in the Video Indexer developer portal.](images/Hands-onlabstep-by-step-MediaAIimages/media/image20.png "Select PRODUCTS")

6. Select the **Authorization** product link.

    ![The Authorization link is highlighted under Products.](images/Hands-onlabstep-by-step-MediaAIimages/media/image21.png "Select Authorization")

7. Select the **Product Authorization Subscription** created for you to view the subscription details

    ![The Product Authorization Subscription link text is highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image22.png "Product Authorization Subscription")

### Task 2: Copy Video Indexer API Key

1. While on the **Profile** page of the **Video Indexer Developer Portal**, locate the **Subscription details** for the **Authorization** subscription.

2. Choose the **Show** link next to the **Primary key** to reveal the API key.

    ![The Show link is highlighted next to the Primary key in the displayed subscription information.](images/Hands-onlabstep-by-step-MediaAIimages/media/image25.png "Reveal the API key")

3. Copy the **Primary key** for the **Free preview** subscription and save this for use later.

    ![The visible Primary key is highlighted in the displayed subscription information.](images/Hands-onlabstep-by-step-MediaAIimages/media/image26.png "Copy the primary key")

### Task 3: Copy Video Indexer Account ID

1. In a web browser, navigate to <https://videoindexer.ai> and login.
   
    ```
    www.videoindexer.ai
    ```

2. Click on your **user avatar** image in the top-right of the page, then click on the **Settings** menu item.

    ![The Settings menu is highlighted in the menu for the user avatar in the top right of the site.](images/Hands-onlabstep-by-step-MediaAIimages/media/videoindexer-avatar-menu-settings-link.png "Click Settings menu item")

3. Click on the **Account** tab.

    ![The Account tab is selected](images/Hands-onlabstep-by-step-MediaAIimages/media/videoindexer-settings-tab.png "Click Account")

4. Copy the **Account ID** and save it for use later.

    ![Click the Copy button for the Account ID](images/Hands-onlabstep-by-step-MediaAIimages/media/videoindexer-settings-account-id-copy-button.png "The Copy button is highlighted")

## Exercise 2: Setup video import workflow

    - Duration: 20 minutes

In this exercise, you will set the import workflow for uploading and importing videos using the Video Indexer API.

### Task 1: Create storage account for video files

1. Open a browser window and login to the Azure Portal: <http://portal.azure.com>.

2. In the menu, select **+Create a resource**, then **Storage** and **Storage account**.

    ![+New is highlighted and labeled 1 in the navigation pane of the Microsoft Azure portal; Storage is selected, highlighted and labeled 2 in the middle; and Storage account is highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image27.png "Select Storage account")

3. On the **Create storage account** blade in the **Basics** tab, enter the following values:

    - Under **Subscription**, locate **Resource group** select **Create new**

    ![The information above is entered and highlighted on the Create storage account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image28.1.png "Configure the settings on the Create storage account blade")

    - Name: **enter a unique name**

    - Replication: **Locally-redundant storage (LRS)**

    - Resource Group: **ContosoVideo**

    - Location: **Choose the location closest to you.**

    ![The information above is entered and highlighted on the Create storage account blade, on the Basics tab.](images/Hands-onlabstep-by-step-MediaAIimages/media/image28.2.png "Configure the settings on the Create storage account blade, on the Basics tab.")

4. Select **Review + Create**, review the details then select **Create**.
       ![In the Create Storage blade, the Review + create tab and create button are selected.](images/Hands-onlabstep-by-step-MediaAIimages/media/image28.3.png "Review the settings on the Create storage account blade, on the Review + create tab.")
5. In the menu, choose **Resource groups**, then select the **ContosoVideo** Resource group, then pick the **Storage Account** that was just created.

    ![Resource groups is highlighted and labeled 1 in the navigation pane of the Microsoft Azure portal; the ContosoVideo Resource group is selected, highlighted and labeled 2 to the right; Overview is selected to the right; and the Storage Account that was just created is highlighted and labeled 3 on the far right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image29.png "Select the Storage account")

6. On the **Storage account** blade, choose the **Blobs** link under Services to get started creating a new Blob Container.

    ![Overview is selected on the left side of the Storage account blade and the Blobs link is highlighted under Services.](images/Hands-onlabstep-by-step-MediaAIimages/media/image30.png "Start creating a new Blob Container")

7. Select the **+Container** button.

    ![+Container is highlighted on the Blob service blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image31.png "Add a container")

8. Enter **video** for the name of the new Container, leave the Public access level set to **Private**, then choose **OK**.
  
    ![The values above are highlighted on the Blob service blade and OK is highlighted at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image32.png "Enter the values for the new container")

9. In the left pane of the Storage Account blade, select **Access keys**, then copy the "primary" **key1**. Save this for reference later.

    ![Access keys is selected and highlighted under Settings on the Storage Account blade and the value for key1 is highlighted on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image33.png "Copy the 'primary' key1")

### Task 2: Create Azure Logic App to process videos

1. Open a browser window and login to the Azure Portal: <http://portal.azure.com> if you haven't already. 

2. In the menu, select **Create a resource** > **Web** > **Logic App**.

3. On the **Create Logic App** blade, enter the following values:

    - Name: **enter a unique name**
    
    - Resource group: **ContosoVideo**
    
    - Location: **choose the location closest to you**
    
    - Log Analytics: **Off**

    ![Create Logic App fields are filled out.](images/Hands-onlabstep-by-step-MediaAIimages/media/image-new-logic-app.png "Create Logic App fields are filled out.")

4. Click **Create**.

5. In the menu, choose **Resource groups**, then select the **ContosoVideo** Resource group, then pick the **Logic App** that was just created.

6. The **Logic Apps Designer** should open automatically. If it doesn't, click to open the Logic Apps Designer.

7. Scroll down and select the **Blank Logic App** template.

    ![Blank Logic App template is highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/image35.png "Blank Logic App template is highlighted")

8. Start building out the Logic App Workflow by searching for and adding the **Azure Blob Storage - When a blob is added or modified (properties only)** trigger.

    ![Blob storage is entered in the search box of Logic App Designer and the Azure Blob Storage -- When a blob is added or modified (properties only) trigger is highlighted and labeled 2 below.](images/Hands-onlabstep-by-step-MediaAIimages/media/image40.png "Add the Azure Blob Storage trigger")

9. Enter a name in the **Connection Name** field, then select the **Storage Account** that was previously created, then select **Create**.

    ![The value under Connection Name is highlighted and the Storage Account that was previously created is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/image41.png "Select the Storage account")

10. On the **Blob Storage Trigger**, enter the following values:

    - Container: **video**

    - Interval: **1**

    - Frequency: **Minute**

    ![The values above are highlighted on the Blob Storage trigger.](images/Hands-onlabstep-by-step-MediaAIimages/media/image42.png "Configure the Blob Storage trigger")

11. Select **+New step**.

    ![+New step button is shown.](images/Hands-onlabstep-by-step-MediaAIimages/media/image43.png "+New step button is shown.")

12. Search for and add the **Azure Blob Storage -- Create SAS URI by path** action.

    ![Blob storage is entered in the search box in the Choose an action dialog box and Azure Blob Storage -- Create SAS URI by path is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/image44.png "Add the Azure Blob Storage. Create SAS URI by path action")

13. Select the **Blob path** field, then choose to insert the **List of Files Path** parameter into the field from the options that display in the **Dynamic Content** section. This will use the Path of the Blob from the Trigger as the value to use when creating the SAS URI in this Action.

    ![Path is highlighted in the Blob path box and the Path parameter is highlighted among the displayed options.](images/Hands-onlabstep-by-step-MediaAIimages/media/image45.png "Select the Path parameter")

14. Choose **+ New step** and search for **Video Indexer** then select the **Get Account Access Token** Action.

    ![Get account access token is entered in the search box in the choose an action dialog box and Video Indexer -- get account access token is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/logicapps-action-video-indexer-get-account-access-token-select.png "Select the Video Indexer get account access token action")

15. For Connection Name, enter **contosovideo-videoindexconnection** , for API Key, enter the API key from when the video index account was set up. Then click **Create**.

    ![Get account access token is entered in the search box in the choose an action dialog box and Video Indexer -- get account access token is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/video_indexer_api_key.png "Select the Video Indexer get account access token action")

16. Enter the following values for the **Video Indexer -- Get account access token** Action:

    - Location: **trial**
    - Account ID: **Select your Video Indexer Account ID from the dropdown.**
    - Allow Edit: **Yes**

    ![Get account access token connector has properties configured.](images/Hands-onlabstep-by-step-MediaAIimages/media/logicapps-action-video-indexer-get-account-access-token-fields.png "Get access token connector has properties configured.")

17. Choose **+New step** and select the **Video Indexer -- Upload video and index** Action.

    ![Upload video and index is entered in the search box in the choose an action dialog box and Video Indexer -- upload video and index (using a URL) is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/image46.png "Select the Video Indexer and upload video and index (using a URL) action")

18. Enter the following values for the **Video Indexer -- Upload video and index (using a URL)** action:

    - Location: **trial**
    - Account ID: **Select your Video Indexer Account ID from the dropdown**.
    - Access Token: **Choose the **Access Token** output from the **Get Account Access Token** action.**

        ![Access token field is highlighted to be set to the 'Access token' value from the previous Get account access token connector.](images/Hands-onlabstep-by-step-MediaAIimages/media/image178.png "Access token field is highlighted to be set to the 'Access token' value from the previous Get account access token connector.")

    - Video Name: Choose the **List of Files DisplayName** parameter from the **When one or more blobs are added or modified** action.

        ![DisplayName is highlighted in the Video Name box and the DisplayName parameter is highlighted among the displayed options.](images/Hands-onlabstep-by-step-MediaAIimages/media/image49.png "Select the DisplayName parameter")

    - Video URL: Choose the **Web URL** parameter from the **Create SAS URI by path** action.

        ![Web URL is highlighted in the Video URL box and the Web URL parameter is highlighted among the displayed options.](images/Hands-onlabstep-by-step-MediaAIimages/media/image48.png "Select the Web URL parameter")

19. Save the Logic App.

    ![The Save icon is selected.](images/Hands-onlabstep-by-step-MediaAIimages/media/image51.png "Save the Logic App")

20. Click the **Run** button to start the Logic App.

    ![The Run icon is selected.](images/Hands-onlabstep-by-step-MediaAIimages/media/image52.png "Run the Logic App")

## Exercise 3: Enable admin website to upload videos

Duration: 45 minutes

In this exercise, you will wire up the Admin website to enable Video Upload functionality.

### Task 1: Provision Cosmos DB Account

1. In the menu, choose **+Create a resource**, then **Databases**, then **Azure Cosmos DB**.

    ![Create a resource is highlighted and labeled 1 in the navigation pane of the Microsoft Azure portal; Databases is selected, highlighted and labeled 2 in the middle; and Azure Cosmos DB is highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image57.png "Select Azure Cosmos DB")

2. On the **Create Azure Cosmos DB account** blade in the **Basics** tab, enter or confirm the following values:

    - Account Name: **enter a unique name**

    - API: **Core SQL**

    - Resource Group: **ContosoVideo**

    - Location: **Choose the same location as previously.**
  
    ![The values above are highlighted on the Azure Cosmos DB New account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image58.png "Configure Azure Cosmos DB New account settings")

3. Choose **Review + Create** then **Create**.

4. In the menu, select **Resource groups**, then select the **ContosoVideo** Resource group, then choose **Cosmos DB Account** that was just created.

    ![Resource groups is highlighted and labeled 1 in the navigation pane of the Microsoft Azure portal; the ContosoVideo Resource group is selected, highlighted and labeled 2 in the middle; and the Cosmos DB Account that was just created is selected, highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image59.png "Select the Cosmos DB Account")

5. On the **Cosmos DB Account** blade, select **Data Explorer**.

    ![Data Explorer is selected and highlighted on the left side of the Cosmos DB Account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image60.png "Select Data Explorer")

6. Choose **New Container**.

   ![The New Container icon is highlighted on the top-right side of the Cosmos DB Account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image61.png "Select New Container")

7. On the **Add Container** pane, enter the following values:

    - Database id: **learning**

    - Container Id: **videos**

    - Partition Key: **/videoId**

    - Throughput: **400**

    ![The values above are highlighted in the Add Collection pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image62.png "Configure Add Collection settings")

8. Select **OK**.

### Task 2: Integrate Cosmos DB into admin web app

1. Within the folder where the exercise files have been extracted to, open the **ContosoLearning.sln** solution within Visual Studio 2017.

    ![This PC is selected in the navigation pane of File Explorer and ContosoLearning.sln is highlighted on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image63.png "Select the ContosoLearning.sln solution")

2. Within Solution Explorer, locate the **ContosoLearning.Data** project, then right-click the project and choose **Manage NuGet Packages...**

    ![The ContosoLearning.Data project is selected and highlighted in Solution Explorer and Manage NuGet Packages is selected and highlighted in the shortcut menu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image64.png "Select Manage NuGet Packages")

3. Within the **NuGet Package Manager**, choose **Browse**, then search for **Microsoft.Azure.DocumentDB**.

    ![Browse is highlighted and labeled 1 in NuGet Package Manager and Microsoft.Azure.DocumentDB is entered in the search box and labeled 2.](images/Hands-onlabstep-by-step-MediaAIimages/media/image65.png "Search for Microsoft.Azure.DocumentDB")

4. Select the **Microsoft.Azure.DocumentDB** NuGet Package, then choose **Install**.

    ![The Microsoft.Azure.DocumentDB NuGet Package is selected, highlighted and labeled 1 on the left and Install is highlighted and labeled 2 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image66.png "Install the Microsoft.Azure.DocumentDB NuGet Package")

5. Choose **OK** on the **Preview Changes** dialog.

    ![This is a screenshot of the Preview Changes dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image67.png "Preview Changes dialog box screenshot")

6. Select **I Accept** on the **License Acceptance** dialog.

    ![This is a screenshot of the License Acceptance dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image68.png "License Acceptance dialog box screenshot")

7. Within Solution Explorer, locate the **ContosoLearning.Data** project and open the **VideoRepository.cs** file. This is the source code file that will contain the source code for interactive with the Cosmos DB Database and Collection within this solution. This data access layer (DAL) implementation uses a simplified Repository pattern.

    ![The VideoRepository.cs file is selected under the ContosoLearning.Data project in Solution Explorer.](images/Hands-onlabstep-by-step-MediaAIimages/media/image69.png "Open the VideoRepository.cs file")

8. Add the following **using** statements to the top of the **VideoRepository.cs** file:

    ```
        using Microsoft.Azure.Documents.Client;
        using Microsoft.Azure.Documents.Linq;
    ```

9. Add the following method named **createDocumentClient()** to the **VideoRepository** class. This is a reusable method that will be used by a couple different methods within the class to instantiate a new DocumentClient class instance for working with Cosmos DB.

    ```
        private DocumentClient createDocumentClient()
        {
            return new Microsoft.Azure.Documents.Client.DocumentClient(
                new Uri(_cosmosDbAuthInfo.Endpoint),
                _cosmosDbAuthInfo.AuthKey
                );
        }
    ```

10. Modify the code for the **GetAll()** method to the following to load all Documents from Cosmos DB and return them as a list object.

    ```
        var list = new List<Video>();

        using (var documentClient = this.createDocumentClient())
        {
            var documentQuery = documentClient.CreateDocumentQuery<Video>(
                UriFactory.CreateDocumentCollectionUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection)
                ).AsDocumentQuery();

            while (documentQuery.HasMoreResults)
            {
                list.AddRange(
                    await documentQuery.ExecuteNextAsync<Video>()
                    );
            }
        }

        return list;
    ```

11. Notice that the above code references a **\_cosmosDbAuthInfo** object. This object is pre-populated with the necessary values for the Cosmos DB Account and Collection by loading from **AppSettings**. These AppSettings will be populated later.

12. Add the below using statement and then Replace the body of the **Get(string id)** method with the following code that will retrieve a single Document by ID.
    ```
    using Microsoft.Azure.Documents;
    ```


    ```
        using (var documentClient = this.createDocumentClient())
        {
            var response = await documentClient.ReadDocumentAsync<Video>(
                UriFactory.CreateDocumentUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection, id), new RequestOptions() { PartitionKey = new PartitionKey(id) }
                );

            return response.Document;
        }
    ```

13. Replace the body of the **Delete(string videoId)** method with the following code that will delete a single document by ID.

    ```
        using (var documentClient = this.createDocumentClient())
        {
            await documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection, videoId)
                );
        }
    ```

14. Add the following code to the **Insert(Video video)** method in place of the *"// Code Here"* comment. This code will insert a new Document into the Cosmos DB Collection:

    ```
        using (var documentClient = this.createDocumentClient())
        {
            await documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_cosmosDbAuthInfo.Database, _cosmosDbAuthInfo.Collection),
                Video
                );
        }
    ```

15. Save the file.

### Task 3: Integrate file upload into admin web app

1. Open the **ContosoLearning.sln** solution within Visual Studio 2017.

2. Within Solution Explorer, locate and expand the **ContosoLearning.Web.Admin** project.

    ![The ContosoLearning.Web.Admin project is selected in Solution Explorer.](images/Hands-onlabstep-by-step-MediaAIimages/media/image70.png "Expand the ContosoLearning.Web.Admin project")

3. Expand the **Controllers** folder and open the **HomeController.cs** file.

    ![The HomeController.cs file under the Controllers folder in Solution Explorer.](images/Hands-onlabstep-by-step-MediaAIimages/media/image71.png "Open the HomeController.cs file")

4. Add the following using statements to the top of the **HomeController.cs** file:

    ```
        using Microsoft.WindowsAzure.Storage;
        using System.Configuration;
    ```

5. Within the **HomeController**, locate the **Upload** method with the **HttpPost** attribute.

    ![The Upload method with the HttpPost attribute is displayed in the HomeController file.](images/Hands-onlabstep-by-step-MediaAIimages/media/image72.png "HomeController screenshot")

6. Paste in the following code where the comment states *"Upload Video File to Blob Storage"*. This code will take the file uploaded to the Action Method via an HTTP Post and upload that file to the Blob Storage Account.

    ```
        // Load Connection String to Azure Storage Account
        var videoConnString = ConfigurationManager.ConnectionStrings["videostorage"].ConnectionString;
        if (string.IsNullOrWhiteSpace(videoConnString))
        {
            throw new Exception("The 'videostorage' Connection String is NOT set");
        }

        // Get reference to the Blob Container to upload to
        var storageAccount = CloudStorageAccount.Parse(videoConnString);
        var blobClient = storageAccount.CreateCloudBlobClient();
        var container = blobClient.GetContainerReference("video");
        await container.CreateIfNotExistsAsync();

        // Upload Video file to Blob Storage
        var blob = container.GetBlockBlobReference(videoId);
        await blob.UploadFromStreamAsync(file.InputStream);
    ```

7. Paste in the following code where the comment states *"Save Video info to Cosmos DB..."*. This code will use the VideoRepository to save a new Document to Cosmos DB for the newly uploaded video.

    ```
        // Save new Document to Cosmos DB for this Video
        var videoRepo = VideoRepositoryFactory.Create();
        var video = new Video();
        video.Id = videoId;
        video.Title = model.Title;
        video.Description = model.Description;
        video.ProcessingState = "Processing";
        video.ProcessingProgress = "0%";
        await videoRepo.Insert(video);
    ```

8. Save the file.

### Task 4: Add ability to delete video

1. Within Solution Explorer, expand the **ContosoLearning.Web.Admin** project, then right-click on **References,** then choose **Manage NuGet Packages...**

    ![References is selected, highlighted and labeled 1 under the ContosoLearning.Web.Admin project and Manage NuGet Packages is selected, highlighted and labeled 2 in the shortcut menu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image73.png "Select Manage NuGet Packages")

2. Within the **NuGet Package Manager,** choose **Browse**, then search for **Microsoft.Rest.ClientRuntime**.

    ![Browse and Microsoft.Rest.ClientRuntime are highlighted in NuGet Package Manager.](images/Hands-onlabstep-by-step-MediaAIimages/media/image74.png "Search for Microsoft.Rest.ClientRuntime")

3. Select the **Microsoft.Rest.ClientRuntime** NuGet package, then choose **Install**.

    ![The Microsoft.Rest.ClientRuntime NuGet package is selected, highlighted and labeled 1 on the left and Install is highlighted and labeled 2 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image75.png "Install the Microsoft.Rest.ClientRuntime NuGet package")

4. Select **OK** on the **Preview Changes** dialog.

    ![This is a screenshot of the Preview Changes dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image76.png "Preview Changes dialog box screenshot")

5. Choose **I Accept** on the **License Acceptance** dialog.

    ![This is a screenshot of the License Acceptance dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image77.png "License Acceptance dialog box screenshot")

6. Within the Solution Explorer window, right-click the **ContosoLearning.Web.Admin** project, then choose **Add**, then **REST API Client...**

    ![The ContosoLearning.Web.Admin project is selected, highlighted and labeled 1 in Solution Explorer; Add is selected, highlighted and labeled 2 in the shortcut menu; and REST API Client is selected, highlighted and labeled 3 in the submenu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image78.png "Add a REST API client")

7. Open a new browser window and navigate to the **Video Indexer API** Developer Portal at: <https://api-portal.videoindexer.ai>.

8. In the top navigation, choose the **APIS** menu, then select **Operations**.

    ![The APIS menu is highlighted and labeled 1 at the top of the Video Indexer developer portal and Video Indexer APIs -- Operations is highlighted and labeled 2 below.](images/Hands-onlabstep-by-step-MediaAIimages/media/image79.png "Select Video Indexer APIs -- Operations")

9. Choose **API definition** to expand its menu.

    ![The API definition button is highlighted on the top-right side of the Video Indexer developer portal and the shortcut menu is open.](images/Hands-onlabstep-by-step-MediaAIimages/media/image80.png "Select API definition")

10. Right-click **Open API 2 (JSON)**, then choose **Copy link address** or **Copy Shortcut** depending on your browser to copy the URL for the link to the clipboard.

    ![Open API is highlighted and labeled 1 in the shortcut menu and Copy link address is selected, highlighted and labeled 2 in the submenu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image81.png "Copy the URL for the link")

11. Go back to Visual Studio and paste in the copied **URL** into the **Swagger URL** field of the **Add REST API Client** dialog, set the **Client Namespace** to `ContosoLearning.Web.Admin.VideoIndexer.Operations`, then select **OK**.

    ![Swagger URL is selected in the Add REST API Client dialog box, the copied URL in the Swagger URL box is highlighted and OK is highlighted at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image82.png "Paste the copied URL in the Add REST API Client dialog box")

12. In the top navigation of the **Video Indexer API** Developer Portal, choose the **APIS** menu, then select **Authorization**.

    ![The APIS menu is highlighted and labeled 1 at the top of the Video Indexer developer portal and Video Indexer APIs -- Authorization is highlighted and labeled 2 below.](images/Hands-onlabstep-by-step-MediaAIimages/media/image179.png "Select Video Indexer APIs -- Authorization")

13. Choose **API definition** to expand its menu.

    ![The API definition button is highlighted on the top-right side of the Video Indexer developer portal and the shortcut menu is open.](images/Hands-onlabstep-by-step-MediaAIimages/media/image180.png "Select API definition")

14. Right-click **Open API 2**, then choose **Copy link address** or **Copy Shortcut** depending on your browser to copy the URL for the link to the clipboard.

    ![Open API is highlighted and labeled 1 in the shortcut menu and Copy link address is selected, highlighted and labeled 2 in the submenu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image81.png "Copy the URL for the link")

15. Go back to Visual Studio, then within the Solution Explorer window, right-click the **ContosoLearning.Web.Admin** project, then choose **Add**, then **REST API Client...**

    ![The ContosoLearning.Web.Admin project is selected, highlighted and labeled 1 in Solution Explorer; Add is selected, highlighted and labeled 2 in the shortcut menu; and REST API Client is selected, highlighted and labeled 3 in the submenu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image78.png "Add a REST API client")

16. Paste in the copied **URL** into the **Swagger URL** field of the **Add REST API Client** dialog, set the **Client Namespace** to `ContosoLearning.Web.Admin.VideoIndexer.Authorization`, then select **OK**.

    ![Swagger URL is selected in the Add REST API Client dialog box, the copied URL in the Swagger URL box is highlighted and OK is highlighted at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image181.png "Paste the copied URL in the Add REST API Client dialog box")

17. Within the **HomeController.cs** file within the **ContosoLearning.Web.Admin** project, locate the **Delete(string id)** Action Method and replace its contents with the following source code that will delete the Video from both Blob Storage and Cosmos DB.

    ```
        // ======================================================================
        // Delete document from Cosmos DB
        // ======================================================================

        var videoRepo = VideoRepositoryFactory.Create();

        var video = await videoRepo.Get(id);

        await videoRepo.Delete(id);

        // ======================================================================
        // Delete files from Blob Storage
        // ======================================================================

        // Load Connection String to Azure Storage Account
        var videoConnString = ConfigurationManager.ConnectionStrings["videostorage"].ConnectionString;
        if (string.IsNullOrWhiteSpace(videoConnString))
        {
            throw new Exception("The 'videostorage' Connection String is NOT set");
        }

        // Get reference to the Blob Container to upload to
        var storageAccount = CloudStorageAccount.Parse(videoConnString);
        var blobClient = storageAccount.CreateCloudBlobClient();

        // Get reference to 'video' container
        var videoContainer = blobClient.GetContainerReference("video");
        await videoContainer.CreateIfNotExistsAsync();

        // Delete Video file from Blob Storage
        var videoBlob = videoContainer.GetBlockBlobReference(id);
        await videoBlob.DeleteAsync();
    ```

18. After the above code, within the **Delete** method, paste in the following code that will also delete the video from **Video Indexer** when it's deleted from the application.

    ```
    var videoIndexerLocation = "trial";
    var videoIndexerTokenCredentials = new Microsoft.Rest.TokenCredentials(
            ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]
        );
    var videoIndexerAuthClient = new VideoIndexer.Authorization.AuthorizationClient(videoIndexerTokenCredentials);

    // Get Video Indexer Account Id
    var accountsResponse = await videoIndexerAuthClient.GetAccountsWithHttpMessagesAsync(videoIndexerLocation);
    dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(await accountsResponse.Response.Content.ReadAsStringAsync());
    var videoIndexerAccountId = accounts[0].id as string;

    // Get Video Indexer Access Token
    var accountAccessTokenResponse = await videoIndexerAuthClient.GetAccountAccessTokenWithHttpMessagesAsync(videoIndexerLocation, videoIndexerAccountId, true);
    var accountAccessToken = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(await accountAccessTokenResponse.Response.Content.ReadAsStringAsync());

    // Delete Video from Video Indexer Account
    var videoIndexerClient = new VideoIndexer.Operations.OperationsClient(videoIndexerTokenCredentials);
    var response = await videoIndexerClient.DeleteVideoWithHttpMessagesAsync(videoIndexerLocation, videoIndexerAccountId, video.VideoId, accountAccessToken);

    return RedirectToAction("Index");
    ```

19. Save the file.

### Task 5: Deploy admin website to an Azure Web App

1.  Within Solution Explorer, right-click the **ContosoLearning.Web.Admin** project, then choose **Publish...**

    ![The ContosoLearning.Web.Admin project is selected and highlighted in Solution Explorer and Publish is selected and highlighted in the shortcut menu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image83.png "Select Publish")

2. Select **App Service**, then select the **Create New** radio button, then choose **Publish**.

    ![Microsoft Azure App Service is selected, highlighted and labeled 1; the Create New radio button is selected, highlighted and labeled 2; and the Publish button is highlighted and labeled 3.](images/Hands-onlabstep-by-step-MediaAIimages/media/image84.png "Create a new Azure App service")

3.  On the **Create App Service** dialog, select the **ContosoVideo** Resource Group, then select **New** next to **Hosting Plan**.
 
    ![ContosoVideo is highlighted and labeled 1 in the Resource Group box in the Create App Service dialog box and New is highlighted and labeled 2 next to the App Service Plan box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image85.png "Create a new App Service Plan for the ContosoVideo Resource Group")

4.  On the **Configure Hosting Plan** dialog, enter a valid name into the **Hosting Plan** name field, then select the **Location** that you used previously for this lab, then choose **OK**.

    ![The App Service Plan and Location values are highlighted in the Configure App Service Plan dialog box and OK is highlighted at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image86.png "Configure the App Service Plan")

5.  Choose **Create**.

    ![The Create button is highlighted at the bottom of the Create App Service dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image87.png "Select Create")

6.  Once the application has finished deploying to the Azure Web App, a new browser window will be opened and navigated to the web app running in Azure.

7.  You should now have an Error screen for the app. This is normal since the AppSettings haven't been configured yet.
 
    ![This is a screenshot of the Error screen for the app.](images/Hands-onlabstep-by-step-MediaAIimages/media/image88.png "View the Error screen")

### Task 6: Configure application settings

1.  Open the Azure Portal, select **Resource groups** in the menu, then choose the **ContosoVideo** Resource Group, then select the **Azure Cosmos DB Account** that was created previously.

    ![Resource groups is highlighted and labeled 1 in the navigation pane of the Azure portal; the ContosoVideo Resource Group is selected, highlighted and labeled 2 in the middle; and the Azure Cosmos DB Account that was created previously is selected, highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image59.png "Select the Azure Cosmos DB Account")

2.  On the **Azure Cosmos DB Account** blade, select **Keys**.
 
    ![Keys is selected on the Azure Cosmos DB Account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image89.png "Select Keys")
    

3.  Copy the **URI** and **Primary Key** for the Cosmos DB Account for use later.
 
    ![The URI and Primary Key values for the Cosmos DB Account are highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image90.png "Copy the URI and Primary Key")

4.  Choose **Resource groups** in the menu, then choose the **ContosoVideo** Resource Group, then choose on the **Storage Account** created in Exercise 2.

    ![Resource groups is highlighted and labeled 1 in the navigation pane of the Azure portal; the ContosoVideo Resource Group is selected, highlighted and labeled 2 in the middle; and the Storage Account that was created previously is highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image29.png "Select the Storage Account created in Exercise 2")

5.  On the **Storage account** blade, select **Access keys**.

    ![Access keys is selected on the Storage account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image91.png "Select Access keys")

6.  Copy the **Connection String** for **Key 1** for use later.

    ![Access keys is selected on the left side of the Storage account blade and the Connection String value for Key 1 is highlighted on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image92.png "Copy the Connection String for Key 1")

7. Select **Resource groups** in the menu, then choose the **ContosoVideo** Resource Group, then select **Azure Web App**.

    ![Resource groups is highlighted and labeled 1 in the navigation pane of the Azure portal; the ContosoVideo Resource Group is selected, highlighted and labeled 2 in the middle; and the Azure Web App is highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image93.png "Select the Azure Web App")

8.  On the **App Service** blade, select **Configuration**.

    ![Configuration is selected and highlighted on the App Service blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image94.png "Select Configuration")

9.  On the **Application settings** pane, scroll down to the **App settings** section.

    ![This is a screenshot of the App settings section on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image95.png "View the App settings section")

10. Add a new App Setting with the Key of **CosmosDB\_Endpoint** with the Value set to the **Cosmos DB Account URI** that was copied.

    ![The CosmosDB\_Endpoint value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image96.png "Add a new App Setting with the Key of CosmosDB_Endpoint")

12. Add a new App Setting with the Key of **CosmosDB\_AuthKey** with the Value set to the **Cosmos DB Account Primary Key** that was copied.

    ![The CosmosDB\_AuthKey value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image97.png "Add a new App Setting with the Key of CosmosDB_AuthKey")

12. Add a new App Setting with the Key of **CosmosDB_Database** with the Value set to **learning**.

    ![The CosmosDB\_Database value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image98.png "Add a new App Setting with the Key of CosmosDB_Database")

13. Add a new App Setting with the Key of **CosmosDB_Collection** with the Value set to **videos**.

    ![The CosmosDB\_Collection value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image99.png "Add a new App Setting with the Key of CosmosDB_Collection")

14. Add a new App Setting with the Key of **VideoIndexerAPI\_Key** with the Value set to the **Video Indexer API Key** that was copied previously.
 
    ![The VideoIndexerAPI\_Key value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image100.png "Add a new App Setting with the Key of VideoIndexerAPI_Key")

15. Locate the **Connection strings** section and add a new Connection String with the following values:

    - Name: **videostorage**

    - Value: **Paste in the Storage Account connection string copied previously.**

    - Type: **Custom**

    ![The settings above are entered in the Connection strings section.](images/Hands-onlabstep-by-step-MediaAIimages/media/image101.png "Configure Connection strings settings")

16. Select **Save**.

    ![Save is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image102.png "Select Save")

17. Refresh the browser with the admin web app running in it. If you closed it, then open it up again. You will now see that the application is loading without error.

    ![This is a screenshot of the Admin Web App page.](images/Hands-onlabstep-by-step-MediaAIimages/media/image103.png "Admin Web App screenshot")

18. **Don't upload a video through the Admin app yet.** We still need to finish setting up the back-end of the application.

## Exercise 4: Update video status when processing is complete

Duration: 20 minutes

In this exercise, you will integrate an Azure Function with the Logic App Workflow so that the Azure Cosmos DB database is updated when a video is finished being processed within Video Indexer.

### Task 1: Create Azure Function

1.  Open **Azure Portal**.

2.  Select the **Create a Resource**, **Compute** then **Function App**
 
    ![Create Resource, Compute and Function app are highlighted and numbered.](images/Hands-onlabstep-by-step-MediaAIimages/media/image34.png "Create a new function in Azure Portal")

3.  On the **Function App Create** dialog, Fill in the following fields.

    - App Name: **contosovideofunction** 
    - Resource Group: **Use existing** then select **ContosoVideo**
    - Location: Use the **same location** as the other apps.
    - Storage: **Use existing** and select the storage account created earlier in this lab.

    Click **Create**.
 
    ![Cloud is highlighted and labeled 1 on the left side of the New Project dialog box; the Azure Functions project template is selected, highlighted and labeled 2 in the middle; and OK is highlighted and labeled 3 at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image104.png "Select the Azure Functions project template")

4. Click on  **Resource Groups**, then on **ContosoVideo**, then click the new function app **contosovideofunction** to open the resource.

    ![Resource Group, ContosoVideo and ContosoFunctionApp are highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image182.png "Select the new function app")

5.  Within the **contosovideofunction** pane, click **Function app settings** to configure the function app.

    ![The FunctionApp project is selected, the Function app settings are highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image105.png "Select Function app settings")

6.  In the **Runtime version** section, select the **~1** option.

    ![Runtime version ~1 is selected.](images/Hands-onlabstep-by-step-MediaAIimages/media/image106.png "Select the runtime version")

7.  On the same window, click the **+** sign to open the **New Azure Function** dialog.

    ![Generic WebHook is selected and highlighted in the New Azure Function dialog box and OK is highlighted at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image107.png "Select Generic WebHook")

8.  Within the **Get Started** pane, locate and click the **Custom Function** link.

    ![The Custom function link is highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image108.png "Select Custom function")

9. In the **Choose a template** pane, locate the **HTTP Trigger** template, click on **C#** to create the template.
 
    ![HTTP trigger template with C# highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/image109.png "Click the C# option in HTTP trigger")

10. In the **HTTP trigger** pane, under **New Function**, Name the function Function1 and click **Create**.
    
    ![HTTP trigger template with C# highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/image1099.png "Click the C# option in HTTP trigger")

11. In the code for the **Function1**  above the run method, update the using statements to match the code below.

    ```
    #r "Microsoft.Azure.Documents.Client"
    #r "Microsoft.Azure.WebJobs.Extensions.DocumentDB"
    #r "Microsoft.Azure.WebJobs"
    #r "Microsoft.Azure.WebJobs.Host"
    #r "Newtonsoft.Json"

    using System;
    using System.Net;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    ```

12. Below the code for the **Run** method, add the class below **Input** with the following two properties:

    ```
    public class Input
    {
        public string documentId { get; set; }
        public string videoId { get; set; }
    }
    ```

13. Save the file.

14. On the right side of the pane select **View files**.
    
    ![View files highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/view_files.png "CLick View files")
    
15. Select **function.json** to view the bindings.
    
    ![function.json highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/open_function.png "CLick the function.json")

16. In the function.json code add the following section:
    ```
         ,
        {
        "name": "inputDocument",
        "type": "documentDB",
        "id": "tempValue",
        "partitionKey": "tempValue",
        "databaseName": "learning",
        "collectionName": "videos",
        "createIfNotExists": false,
        "connection": "contosovideodb_DOCUMENTDB",
        "direction": "out"
        }
    ```
    The bindings should now look like this:
    ```
        {
        "bindings": [
            {
            "authLevel": "function",
            "name": "req",
            "type": "httpTrigger",
            "direction": "in",
            "methods": [
                "get",
                "post"
            ]
            },
            {
            "name": "$return",
            "type": "http",
            "direction": "out"
            },
            {
            "name": "inputDocument",
            "type": "documentDB",
            "id": "tempValue",
            "partitionKey": "tempValue",
            "databaseName": "learning",
            "collectionName": "videos",
            "createIfNotExists": false,
            "connection": "contosovideodb_DOCUMENTDB",
            "direction": "out"
            }
        ],
        "disabled": false
        }
    ```
17. Save the file.

18. Click the **run.csx** file to return to the C# code.
    
    ![function.json highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/open_csharp_code.png "CLick the function.json")

19. Update signature of the **Run** method to contain a **binder** parameter of type **Binder**. This also adds the **DocumentDb** attributes to the binding we just added in the **JSON** file. **Binding** will allow the Azure Function to bind the DocumentDb document with incoming values and update the status of the process from Video Indexer.

    >**Note**: The first 2 parameters of the **DocumentDB** attribute define to connect to the "*videos*" Cosmos DB Collection within the "*learning*" database. And the value of "*{documentId}*" will enable it to retrieve the Document whose ID is set to the same value of the "*documentId*" value passed into the method via the HTTP call. The "*ConnectionStringSetting"* parameter sets the name of the App Setting that will store the Cosmos DB Connection String.
    
    ```
    public static async Task<object> Run(Binder binder,
        HttpRequestMessage req,
        [DocumentDB(databaseName: "learning", collectionName: "videos", Id = "{documentId}", PartitionKey = "{documentId}", ConnectionStringSetting = "contosovideodb_DOCUMENTDB")]  dynamic inputDocument,
        TraceWriter log)
    ```

20. Add the code to get the query string values and bind the incoming values to the DocumentDb document **inputDocument** and to save the **input.VideoId** indexer id to the document.

    ```
        Input input = new Input
    {
        DocumentId = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "documentId", true) == 0).Value,
        VideoId = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "videoId", true) == 0).Value
    };
        inputDocument = await binder.BindAsync<Object>(new DocumentDBAttribute("learning", "videos")
    {
        ConnectionStringSetting = "contosovideodb_DOCUMENTDB",
        Id = input.DocumentId,
        PartitionKey = input.DocumentId
    });
        // Save the Video Indexer 'videoId' to the Cosmos DB Document
        inputDocument.videoId = input.VideoId;
    ```
21. Replace the return code of the **Run** method with the following code that checks the necessary parameters are passed in to the method:

    ```
        log.Info("Function triggered...");

        // Log the parameters passed in through the Request Body
        log.Info($"DocumentId: {input.documentId}");
        log.Info($"VideoId: {input.videoId}");

        if (string.IsNullOrEmpty(input.videoId) || string.IsNullOrEmpty(input.documentId))
        {
            log.Error("DocumentId and/or VideoId parameter missing!");
            return req.CreateResponse(HttpStatusCode.BadRequest, $"Please pass a 'videoId' and 'documentId' in the Http request body");
        }
        return req.CreateResponse(HttpStatusCode.OK, "Success");
    ```

22. Add a helper model class **VideoProcessingState** to get the values from the **Video Indexer** at the very end of the file.
    ```
    public class VideoProcessingState
    {
        public string state { get; set; }

        public string ErrorType { get; set; }

        public string Message {get; set;}

        public videostate[] videos { get; set; }

        public class videostate
        {
            public string processingProgress { get; set; }
        }
    }
    ``` 
23. Add a method that uses the VideoProcessingState class and connects to **Video Indexer** just below the **Run** method's ending bracket.

    ```
    private static async Task<VideoProcessingState> GetVideoProcessingState(string videoId, TraceWriter log)
    {
        var client = new HttpClient();

        // Request headers
        string subscriptionKey = Environment.GetEnvironmentVariable("VideoIndexerAPI_Key");//System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"];
           log.Info($"Video Index API Keys: {subscriptionKey}");
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

        // Get Video Indexer Account ID
        var uriAccountsResponse = await client.GetAsync("https://api.videoindexer.ai/auth/trial/Accounts?generateAccessTokens=true&allowEdit=true");
        var jsonUriAccountsResponse = await uriAccountsResponse.Content.ReadAsStringAsync();
        dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(jsonUriAccountsResponse);
        var videoIndexerAccountId = accounts[0].id;
        var accessToken = accounts[0].accessToken;

        //Get Video Index
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        var uri = $"https://api.videoindexer.ai/trial/Accounts/{videoIndexerAccountId}/Videos/{videoId}/Index?accessToken={accessToken}";

        var response = await client.GetAsync(uri);

        var content = await response.Content.ReadAsStringAsync();

        log.Info($"Processing State JSON: {content}");

        return JsonConvert.DeserializeObject<VideoProcessingState>(content);
    }
    ```
24. The resulting **run.csx** file should have the following code:

    ```
    #r "Microsoft.Azure.Documents.Client"
    #r "Microsoft.Azure.WebJobs.Extensions.DocumentDB"
    #r "Microsoft.Azure.WebJobs"
    #r "Microsoft.Azure.WebJobs.Host"
    #r "Newtonsoft.Json"

    using System;
    using System.Net;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;

    [FunctionName("Function1")]
    public static async Task<object> Run(Binder binder,
        HttpRequestMessage req,
        [DocumentDB(databaseName: "learning", collectionName: "videos", Id = "{documentId}", PartitionKey = "{documentId}", ConnectionStringSetting = "contosovideodb_DOCUMENTDB")]  dynamic inputDocument,
        TraceWriter log)    
    {
    // get query string values
    Input input = new Input
    {
        DocumentId = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "documentId", true) == 0).Value,
        VideoId = req.GetQueryNameValuePairs().FirstOrDefault(q => string.Compare(q.Key, "videoId", true) == 0).Value
    };

    // Bind attributes to DocumentDb
    inputDocument = await binder.BindAsync<Object>(new DocumentDBAttribute("learning", "videos")
    {
        ConnectionStringSetting = "contosovideodb_DOCUMENTDB",
        Id = input.DocumentId,
        PartitionKey = input.DocumentId
    });


    // Save the Video Indexer 'videoId' to the Cosmos DB Document
    inputDocument.videoId = input.VideoId;

    // Load Video Processing State
    dynamic processingState = await GetVideoProcessingState(input.VideoId, log);

            log.Info($"Video Processing State: {processingState.state}");
            log.Info($"Video Processing Progress: {processingState.videos[0].processingProgress}");

            // Save Video Processing State in Cosmos DB Document
            inputDocument.processingState = processingState.state;
            inputDocument.processingProgress = processingState.videos[0].processingProgress;

            log.Info("Function triggered...");

        // Log the parameters passed in through the Request Body
        log.Info($"DocumentId: {input.DocumentId}");
        log.Info($"VideoId: {input.VideoId}");

        if (string.IsNullOrEmpty(input.VideoId) || string.IsNullOrEmpty(input.DocumentId))
        {
            log.Error("DocumentId and/or VideoId parameter missing!");
            return req.CreateResponse(HttpStatusCode.BadRequest, $"Please pass a 'videoId' and 'documentId' in the Http request body");
        }
        return req.CreateResponse(HttpStatusCode.OK, "Success");
    }
    private static async Task<VideoProcessingState> GetVideoProcessingState(string videoId, TraceWriter log)
    {
        var client = new HttpClient();

        // Request headers
        string subscriptionKey = Environment.GetEnvironmentVariable("VideoIndexerAPI_Key");//System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"];
           log.Info($"Video Index API Keys: {subscriptionKey}");
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

        // Get Video Indexer Account ID
        var uriAccountsResponse = await client.GetAsync("https://api.videoindexer.ai/auth/trial/Accounts?generateAccessTokens=true&allowEdit=true");
        var jsonUriAccountsResponse = await uriAccountsResponse.Content.ReadAsStringAsync();
        dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(jsonUriAccountsResponse);
        var videoIndexerAccountId = accounts[0].id;
        var accessToken = accounts[0].accessToken;

        //Get Video Index
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
        var uri = $"https://api.videoindexer.ai/trial/Accounts/{videoIndexerAccountId}/Videos/{videoId}/Index?accessToken={accessToken}";

        var response = await client.GetAsync(uri);

        var content = await response.Content.ReadAsStringAsync();

        log.Info($"Processing State JSON: {content}");

            return JsonConvert.DeserializeObject<VideoProcessingState>(content);
       }

    public class Input
    {
        public string DocumentId { get; set; }
        public string VideoId { get; set; }
    }
    
    public class VideoProcessingState
    {
        public string state { get; set; }

        public string ErrorType { get; set; }

        public string Message {get; set;}

        public videostate[] videos { get; set; }

        public class videostate
        {
            public string processingProgress { get; set; }
        }
    }
    ```
25. Save the changes.

26. Open the **Azure Portal** and navigate to the **Cosmos DB Account** that was previously created.

27. On the **Cosmos DB Account** blade, choose **Keys**.

    ![Keys is highlighted on the Cosmos DB Account blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image124.png "Select Keys")

28. Copy the **PRIMARY CONNECTION STRING** for the Cosmos DB Account.

    ![The PRIMARY CONNECTION STRING value for the Cosmos DB Account is highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image125.png "Copy the PRIMARY CONNECTION STRING")

29. Navigate to the **ContosoVideo** Resource Group, then navigate to the **Azure Function** that was created and published from Visual Studio.

    ![Overview is selected on the left side of the ContosoVideo Resource Group and the Azure Function that was created in Azure Portal is highlighted on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image126_2.png "Select the Azure Function")

30. On the **Azure Function** blade, choose **Configuration** under the Configured features section.

    ![Application settings is highlighted in the Configured features section of the Azure Function blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image127.png "Select Application settings")

31. Scroll down and add a new **Application setting** with the following values:

    - Name: **contosovideodb\_DOCUMENTDB**

    - Value: **Paste in the Cosmos DB Account Connection String that was copied**.

    ![The values above are highlighted under Application settings.](images/Hands-onlabstep-by-step-MediaAIimages/media/image128.png "Add a new Application setting")

32. Add another **Application setting** with the following values:

    - Name: **VideoIndexerAPI\_Key**

    - Value: **Paste in the Video Indexer API Key that was copied previously**.

    ![The values above are displayed under Application settings.](images/Hands-onlabstep-by-step-MediaAIimages/media/image129.png "Add another Application setting")

33. Go back up and select **Save**.

    ![This is a screenshot of the Save icon.](images/Hands-onlabstep-by-step-MediaAIimages/media/image130.png "Select Save")

### Task 2: Update Video State when processing is complete

1. Open the **Logic App Designer** within the Azure Portal for the Logic App that was previously created.

2. Locate the end of the Workflow, after the **Video Indexer -- Upload video and index** action.

    ![+ New step is visible under the Video Indexer -- Upload video and index (using a URL) action at the end of the Workflow.](images/Hands-onlabstep-by-step-MediaAIimages/media/image131.png "Locate the end of the Workflow")

3. Select **+New step**

    ![+ New step is highlighted](images/Hands-onlabstep-by-step-MediaAIimages/media/newstep.png "Select Add a do until")

4. Search for and select the **Until** under the **Control** category of actions. This will be used to periodically check the video processing state and wait until it's finished before moving on with the workflow.

    ![Until button is highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/control-until.png "Select Add a do until")

5. Within the **Until** action, choose the **Add an action** link to add an action within the "until" block.

    ![Add an action is highlighted at the bottom of the Until dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image133.png "Select Add an action")

6. Search for and add a **Video Indexer -- Get Video Index** action.

    ![Get processing state is highlighted in the search box of the Choose an action dialog box and the Video Indexer -- Get Video Index action is highlighted under Actions.](images/Hands-onlabstep-by-step-MediaAIimages/media/image134.png "Add a Video Indexer. Get processing state action")

7. On the **Get Video Index** action, enter the following values:

    - Location: **trial**
    - Account ID: **Select your Video Indexer Account ID**.
    - Video ID: Select the **Video ID** value from the **Upload video and index** action.

    ![The Video Id box and the Video Id parameter value from the Upload video and index (using a URL) action are highlighted on the Get Video Index  action.](images/Hands-onlabstep-by-step-MediaAIimages/media/image135.png "Enter the Video Id parameter value")

    - Access Token: select the **Access Token** value from the **Get Account Access Token** action.

    ![The Get Video Index action has all its properties set as described in this step.](images/Hands-onlabstep-by-step-MediaAIimages/media/image-get-video-index-proeprties-set.png "The Get Video Index action has all its properties set as described in this step.")

8. At the top of the **Until** action, set the check condition to look at the **State** parameter from the **Get Video Index** action and compare that it **is equal to** the value of **Processed**.

    ![The values above are displayed on the Until action.](images/Hands-onlabstep-by-step-MediaAIimages/media/image136.png "Configure Until action settings")

    ![The values above are displayed on the Until action.](images/Hands-onlabstep-by-step-MediaAIimages/media/image183.png "State is equal to Processed")

9. Choose **Add an action** to add another action within the **Until** action, after the **Get processing state** action.

    ![Add an action is highlighted at the bottom of the Until dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image137.png "Select Add an action")

    > **Note**: The Processing State is being polled here so it can easily be added to the database so the percentage complete can be displayed to the end-user within the web app more easily. Without this feature, the best practice would be to configure a callback with the initial Video Indexer call, so it can asynchronously notify when processing is completed.

10. Select the **Choose an Azure function** action of the **Azure Functions** Connector.

    ![The Azure Functions icon is highlighted under Connectors in the Choose an action dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image138.png "Select the Azure Functions Connector")

11. For the Azure Functions action, select the **Azure Function** name that was created previously.

    ![Azure Functions is highlighted under Actions in the Azure Functions dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image140.png "Select the Azure Functions name")

12. Update the **Request Body** field for the Azure Functions Action to contain a JSON object that includes **documentId** and **videoId** values.

    ```
    {"documentId": "", "videoId": "" }
    ```

    ![The values above are in the Request Body box for the Azure Functions Action.](images/Hands-onlabstep-by-step-MediaAIimages/media/image141.png "Update the Request Body box")

13. Modify the JSON properties to have the following values. Also, be sure to remove the empty double quotes `""` from the JSON when adding the new property values as shown below.

    - Set the **documentId** property to the **List of Files Name** parameter from the **Blob Storage -- When one or more blobs are added or modified (metadata only)** action.

    ![The Name parameter of the documentId property is highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image142.png "Select the Name parameter")

    - Set the **videoId** property to the **Video Id** parameter from the **Video Indexer -- Upload and index (using a URL)** action.

    ![The Video Id parameter of the documentId property is highlighted.](images/Hands-onlabstep-by-step-MediaAIimages/media/image143.png "Select the Video Id parameter")

14. Choose **Add an action** to add another action within the **Until** action, after the Azure Functions Action.

    ![Add an action is highlighted at the bottom of the Until dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image144.png "Select Add an action")

15. Search for **delay** and select the **Schedule** action.

    ![Delay is entered in the search box in the Choose an action dialog box and Schedule -- Schedule is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/Schedule.png "Add the Schedule: Delay action")

16. Search for and add a **Schedule -- Delay** action.

    ![Delay is highlighted below.](images/Hands-onlabstep-by-step-MediaAIimages/media/Delay.png "Add the Schedule: Delay action")

17. On the **Delay** action, enter the following values:

    - Count: **30**

    - Unit: **Second**

    ![The values above are highlighted on the Delay action.](images/Hands-onlabstep-by-step-MediaAIimages/media/image146.png "Configure Delay action settings")

18. Scroll down to the bottom or end of the Logic App Workflow and choose **+New step**.
    ![+ New step is highlighted at the end of the Logic App Workflow](images/Hands-onlabstep-by-step-MediaAIimages/media/image147.png "New Step")

19. Choose the **Azure Functions Action**, then select the previous function here that is configured identical to the one previously created within the **Until** loop action. The reason for this is the first one within the **Until** action will periodically update the status of the Video Processing within the Cosmos DB document with each iteration of the loop. This new Azure Function at the end of the Logic App Workflow will update the Video Processing State one final time before the Workflow finished execution.

    ![Azure Function Action is shown](images/Hands-onlabstep-by-step-MediaAIimages/media/function2.png "Azure Function")

20. **Save** the Logic App.

21. Click **Run** to run the Logic App.

## Exercise 5: Add video player to front-end application

Duration: 30 minutes

In this exercise, you will extend the Front-End Application foundation to include a video player and Cognitive Services Insights for the Videos.

### Task 1: Integrate Cosmos DB into front-end application

1.  Open the **ContosoLearning.sln** solution within Visual Studio 2019.

2.  Within Solution Explorer, locate and expand the **ContosoLearning.Web.Public** project, then expand the **Controllers** folder and open the **HomeController.cs** file.

    ![The ContosoLearning.Web.Public project is highlighted in Solution Explorer and the expanded Controllers folder and the HomeController.cs file are highlighted below it.](images/Hands-onlabstep-by-step-MediaAIimages/media/image148.png "Open the HomeController.cs file")

3.  Within the **HomeController** class, locate the **Index()** Action method and replace the methods contents with the following code that uses the **VideoRepository** to load all the Videos from the Cosmos DB Collection and returns the data in the Model so the view can display it in the UI.

    ```
    var model = new HomeIndexModel();

    var videoRepo = VideoRepositoryFactory.Create();

    model.Videos = (from v in await videoRepo.GetAll()
                        orderby v.Title, v.Created
                        select new VideoListModel
                        {
                            Video = v
                        }).ToArray();

    return View(model);
    ```

4.  Locate the **Video(string id)** method. This Action method is used to display the video player for individual videos. Replace the contents of this method with the following code that loads the Video info from the Cosmos DB Collection.

    ```
    var model = new HomeVideoModel();

    var courseRepo = VideoRepositoryFactory.Create();
    model.Video = await courseRepo.Get(id);

    if (model.Video == null)
    {
        throw new Exception("Video not found!");
    }

    // Get Access Token
    var client = new System.Net.Http.HttpClient();
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]);

    var uriAccountsResponse = await client.GetAsync("https://api.videoindexer.ai/auth/trial/Accounts");
    var jsonUriAccountsResponse = await uriAccountsResponse.Content.ReadAsStringAsync();
    dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(jsonUriAccountsResponse);
    var videoIndexerAccountId = accounts[0].id;
    model.AccountId = videoIndexerAccountId.ToString();

    var uriResponse = await client.GetAsync($"https://api.videoindexer.ai/auth/trial/Accounts/{videoIndexerAccountId}/Videos/{model.Video.VideoId}/AccessToken");
    var jsonUriResponse = await uriResponse.Content.ReadAsStringAsync();

    model.AccessToken = jsonUriResponse.Replace("\"", string.Empty);

    return View(model);
    ```

5.  **Save** the file.

### Task 2: Display video thumbnail image

1.  Within Solution Explorer, expand the **Controllers** folder within the **ContosoLearning.Web.Public** project, then open the **HomeController.cs** file.

    ![The HomeController.cs file is selected and highlighted under the Controllers folder in the ContosoLearning.Web.Public project in Solution Explorer.](images/Hands-onlabstep-by-step-MediaAIimages/media/image149.png "Open the HomeController.cs file")

2.  Locate the **Index()** action method within the **HomeController** class.

3.  Paste in the following code immediately before the **return** statement in the **Index()** method. This code will loop through all the Videos and load the **Video Thumbnail URL** for each by calling the **Video Indexer API**.

    ```
    var client = new System.Net.Http.HttpClient();

    // Request headers
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]);

    // Get Video Indexer Account ID
    var uriAccountsResponse = await client.GetAsync("https://api.videoindexer.ai/auth/trial/Accounts");
    var jsonUriAccountsResponse = await uriAccountsResponse.Content.ReadAsStringAsync();

    dynamic accounts = Newtonsoft.Json.Linq.JArray.Parse(jsonUriAccountsResponse);
    var videoIndexerAccountId = accounts[0].id;


    foreach (var v in model.Videos)
    {
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", System.Configuration.ConfigurationManager.AppSettings["VideoIndexerAPI_Key"]);
        // Get Video Indexer Access Token
        var uriResponse = await client.GetAsync($"https://api.videoindexer.ai/auth/trial/Accounts/{videoIndexerAccountId}/Videos/{v.Video.VideoId}/AccessToken");
        var jsonUriResponse = await uriResponse.Content.ReadAsStringAsync();
        var accessToken = jsonUriResponse.Replace("\"", string.Empty);

        var uri = $"https://api.videoindexer.ai/trial/Accounts/{videoIndexerAccountId}/Videos/{v.Video.VideoId}/Index?accessToken={accessToken}";
        var response = await client.GetAsync(uri);
        var json = await response.Content.ReadAsStringAsync();

        dynamic breakdown = Newtonsoft.Json.Linq.JObject.Parse(json);

        var thumbnailId = breakdown?.summarizedInsights?.thumbnailId;
        v.ThumbnailUrl = $"https://api.videoindexer.ai/trial/Accounts/{videoIndexerAccountId}/Videos/{v.Video.VideoId}/Thumbnails/{thumbnailId}?accessToken={accessToken}&format=Jpeg";
    }
    ```

### Task 3: Add video player

1.  Within Solution Explorer, locate and expand the **Views/Home** folder within the **ContosoLearning.Web.Public** project and open the **Video.cshtml** file.

    ![The Video.cshtml file is selected and highlighted under the Views and Home folders in the ContosoLearning.Web.Public project in Solution Explorer.](images/Hands-onlabstep-by-step-MediaAIimages/media/image150.png "Select the Video.cshtml file")

2.  Locate the `[Video Here]` placeholder text within the **Video.cshtml** view.

    ![The "\[Video Here\]" placeholder text is visible in the Video.cshtml file.](images/Hands-onlabstep-by-step-MediaAIimages/media/image151.png "Find the placeholder text")

3.  Replace the placeholder text with the following code that will include the **Video Player** within an IFrame. Notice the **VideoId** property from the Video is appended to the URL within the IFrame to tell Video Indexer which video to play.

    ```
    <iframe width="560" height="315" src="https://www.videoindexer.ai/embed/player/@(Model.AccountId)/@(Model.Video.VideoId)?accessToken=@(Model.AccessToken)" frameborder="0" allowfullscreen></iframe>
    ```

### Task 4: Add video insights

1.  Within the **Video.cshtml** file, locate the `[Insights Here]` placeholder text.

    ![The "\[Insights Here\]" placeholder text is visible in the Video.cshtml file.](images/Hands-onlabstep-by-step-MediaAIimages/media/image152.png "Find the placeholder text")

2.  Replace the placeholder text with the following code that will include the **Video** **Insights** within an IFrame. Notice the **VideoId** property from the Video is appended to the URL within the IFrame to tell Video Indexer which video to display insights for.

    ```
    <iframe style="width: 100%; height: 60em;" src="https://www.videoindexer.ai/embed/insights/@(Model.AccountId)/@(Model.Video.VideoId)?accessToken=@(Model.AccessToken)" frameborder="0" allowfullscreen="true"></iframe>
    ```

### Task 5: Integrate video player and insights together

1.  As coded previously, the Video Player and Insights will display, but are disconnected.

2.  To connect the Video Player and Insights so the insights can update as the video plays, add the following JavaScript file to the bottom of the **Video.cshtml** View below the IFrame code:

    ```
        <script src="https://breakdown.blob.core.windows.net/public/vb.widgets.mediator.js"></script>
    ```

3.  Save the file.

### Task 6: Deploy public website to an Azure Web App

1.  Within Solution Explorer, right-click the **ContosoLearning.Web.Public** project, then select **Publish...**

    ![The ContosoLearning.Web.Public project is selected and highlighted in Solution Explorer and Publish is selected and highlighted in the shortcut menu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image153.png "Select Publish")

2.  Choose **App Service**, then select the **Create New** radio button, then choose **Publish**.

    ![Microsoft Azure App Service is selected, highlighted and labeled 1; the Create New radio button is selected, highlighted and labeled 2; and the Publish button is selected, highlighted and labeled 3.](images/Hands-onlabstep-by-step-MediaAIimages/media/image154.png "Create a new Azure App service")

3.  On the **Create New App Service** dialog, select the **ContosoVideo** Resource Group, then select **New** near **Hosting Plan**.

    ![ContosoVideo is highlighted and labeled 1 in the Resource Group box in the Create App Service dialog box and New is highlighted and labeled 2 next to the App Service Plan box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image155.png "Create a new App Service Plan for the ContosoVideo Resource Group")

4.  On the **Configure Hosting Plan** dialog, enter a valid name into the **Hosting Plan** name field, then select the **Location** that you used previously for this lab, then select **OK**.

    ![The App Service Plan and Location values are highlighted in the Configure App Service Plan dialog box and OK is highlighted at the bottom.](images/Hands-onlabstep-by-step-MediaAIimages/media/image156.png "Configure the App Service Plan")

5.  Choose **Create**.

    ![The Create button is highlighted at the bottom of the Configure App Service Plan dialog box.](images/Hands-onlabstep-by-step-MediaAIimages/media/image157.png "Select Create")

6.  Once the application has finished deploying to the Azure Web App, a new browser window will be opened and navigated to the web app running in Azure.

7.  You should now see an Error screen for the app. This is normal since the AppSettings haven't been configured yet.
 
    ![This is a screenshot of the Error screen for the app.](images/Hands-onlabstep-by-step-MediaAIimages/media/image158.png "View the Error screen")

### Task 7: Configure application settings

1.  Open the **Azure Portal**, choose **Resource groups** in the menu, then select the **ContosoVideo** Resource Group, then choose **Azure Web App** that was just created for the Public website.

    ![Resource groups is highlighted and labeled 1 in the navigation pane of the Azure portal; the ContosoVideo Resource Group is selected, highlighted and labeled 2 in the middle; and the Azure Web App is highlighted and labeled 3 on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image159.png "Select the Azure Web App")

2.  On the **App Service** blade, choose **Configuration**.

    ![Application settings is selected and highlighted on the left side of the App Service blade.](images/Hands-onlabstep-by-step-MediaAIimages/media/image94.png "Select Application settings")

3.  On the **Application settings** pane, scroll down to the **App settings** section.

    ![This is a screenshot of the App settings section in the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image160.png "Application settings screenshot")

4.  Add a new App Setting with the Name of **CosmosDB\_Endpoint** with the Value set to the **Cosmos DB Account URI** that was copied.

    ![The CosmosDB\_Endpoint value is displayed on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image161.png "Add a new App Setting with the Key of CosmosDB_Endpoint")

5.  Add a new App Setting with the Name of **CosmosDB\_AuthKey** with the Value set to the **Cosmos DB Account Primary Key** that was copied.

    ![The CosmosDB\_AuthKey value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image162.png "Add a new App Setting with the Key of CosmosDB_AuthKey")

6.  Add a new App Setting with the Key of **CosmosDB\_Database** with the Value set to **learning**.

    ![The CosmosDB\_Database value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image163.png "Add a new App Setting with the Key of CosmosDB_Database")

7.  Add a new App Setting with the Key of **CosmosDB\_Collection** with the Value set to **videos**.
  
    ![The CosmosDB\_Collection value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image164.png "Add a new App Setting with the Key of CosmosDB_Collection")

8.  Add a new App Setting with the Key of **VideoIndexerAPI\_Key** with the Value set to the **API Key** for the **Video Indexer API** subscription.
  
    ![The VideoIndexerAPI\_Key value is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image165.png "Add a new App Setting with the Key of VideoIndexerAPI_Key")

9.  Select **Save**.
 
    ![Save is highlighted on the Application settings pane.](images/Hands-onlabstep-by-step-MediaAIimages/media/image166.png "Select Save")
    

## Exercise 6: Test the application

Duration: 15 minutes

In this exercise, you will test out the admin and public web applications.

### Task 1: Upload video to admin website

1.  Open a browser window to the **Admin Website** running in Azure Web Apps. You can use the window that was open previously if it's still open.

    ![This is a screenshot of the Admin Website page.](images/Hands-onlabstep-by-step-MediaAIimages/media/image167.png "Admin Website screenshot")

2.  Select the **Add Video** link to begin uploading and adding a new video to the catalog in the application.

    ![The Add Video link is highlighted at the top of the Admin Website page.](images/Hands-onlabstep-by-step-MediaAIimages/media/image168.png "Select Add Video")

3.  Enter some text into the form for the **Title** and **Description** fields, then choose a **Video** file to upload. Choose the "*Introduction-to-the-Azure-Portal\_mid.mp4*" video file downloaded with the student files for this lab, then select **Upload Video**.

    ![The Title, Description and Video fields and the Upload Video button are highlighted on the Add Course page.](images/Hands-onlabstep-by-step-MediaAIimages/media/image169.png "Upload a video file")

    > **Note**: Uploading the video file may take a few minutes depending on your Internet connection.

4.  Once the video file has been uploaded, the homepage of the admin app will load displaying the Video Processing State and Progress.

    ![The Video Processing State and Progress are displayed on the homepage of the Admin app.](images/Hands-onlabstep-by-step-MediaAIimages/media/image170.png "Admin app homepage screenshot")

### Task 2: View video and insights in public website

1.  Open a browser window to the **Public Website** running in Azure Web Apps. If using the window previously opened, just refresh the page to reload it.

2.  While the videos are processing within the **Video Indexer** service, the public website will display the Processing State and Progress.

    ![This is a screenshot of the Public Website displaying the Processing State and Progress.](images/Hands-onlabstep-by-step-MediaAIimages/media/image171.png "Public website screenshot")

3.  Once the videos have finished processing, select the thumbnail image or the title of the video to view that video.

    ![The Thumbnail image and the Title of the Video, Intro to Azure Portal, are displayed and highlighted on the Public Website.](images/Hands-onlabstep-by-step-MediaAIimages/media/image172.png "View the video thumbnail image and title")

4.  On the video player page, select the **Transcript** tab within the Video Insights. This will show you the transcription of the audio from the video.

    ![The Transcript tab is highlighted on the Video Player page.](images/Hands-onlabstep-by-step-MediaAIimages/media/image173.png "View the transcription of the audio from the video")

5.  Notice as the video plays through, the transcript highlights and automatically scrolls to reveal the text in the video.

6.  Hover over the **Video Player**, then hover over the **Closed Captions** icon, then choose on the **En-us** (English) language in the popup menu.

    ![The Closed Captions icon is highlighted and labeled 1 at the bottom of the Azure portal and En-us is highlighted and labeled 2 in the popup menu.](images/Hands-onlabstep-by-step-MediaAIimages/media/image174.png "Select En-us (English)")

7.  **Captions** is being displayed over the video.

    ![An English caption is highlighted at the bottom of the screenshot of the video.](images/Hands-onlabstep-by-step-MediaAIimages/media/image175.png "Captions now display over the video")

8.  Select the **Language** dropdown in the **Video** **Insights** pane, then change the language to **Chinese (Simplified)**.

    ![The Language list is highlighted and labeled 1 in the Video Insights pane and Chinese (Simplified) is highlighted and labeled 2 in the drop-down list.](images/Hands-onlabstep-by-step-MediaAIimages/media/image176.png "Select Chinese (Simplified)")

9.  The **Closed Captions** and the **Transcript** will now be automatically translated into **Chinese (Simplified)**.

    ![A Chinese caption is highlighted at the bottom of the screenshot of the video and the Chinese transcript is highlighted on the right.](images/Hands-onlabstep-by-step-MediaAIimages/media/image177.png "Chinese captions now display over the video")

## After the hands-on lab

Duration: 10 minutes

### Task 1: Delete resources

1. Now that the hands-on lab is complete, go ahead and delete all the Resource Groups that were created for this lab. You will no longer need those resources and it will be beneficial to clean up your Azure Subscription.

You should follow all steps provided *after* attending the Hands-on lab.
