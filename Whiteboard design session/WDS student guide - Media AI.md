![](https://github.com/Microsoft/MCW-Template-Cloud-Workshop/raw/master/Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

<div class="MCWHeader1">
Media AI
</div>

<div class="MCWHeader2">
 Whiteboard design session student guide
</div>

<div class="MCWHeader3">
May 2019
</div>

Information in this document, including URL and other Internet Web site references, is subject to change without notice. Unless otherwise noted, the example companies, organizations, products, domain names, e-mail addresses, logos, people, places and events depicted herein are fictitious and no association with any real company, organization, product, domain name, e-mail address, logo, person, place or event is intended or should be inferred. Complying with all applicable copyright laws is the responsibility of the user. Without limiting the rights under copyright, no part of this document may be reproduced, stored in or introduced into a retrieval system, or transmitted in any form or by any means (electronic, mechanical, photocopying, recording, or otherwise), or for any purpose, without the express written permission of Microsoft Corporation.

Microsoft may have patents, patent applications, trademarks, copyrights, or other intellectual property rights covering subject matter in this document. Except as expressly provided in any written license agreement from Microsoft, the furnishing of this document does not give you any license to these patents, trademarks, copyrights, or other intellectual property.

The names of manufacturers, products, or URLs are provided for informational purposes only and Microsoft makes no representations and warranties, either expressed, implied, or statutory, regarding these manufacturers or the use of the products with any Microsoft technologies. The inclusion of a manufacturer or product does not imply endorsement of Microsoft of the manufacturer or product. Links may be provided to third party sites. Such sites are not under the control of Microsoft and Microsoft is not responsible for the contents of any linked site or any link contained in a linked site, or any changes or updates to such sites. Microsoft is not responsible for webcasting or any other form of transmission received from any linked site. Microsoft is providing these links to you only as a convenience and the inclusion of any link does not imply endorsement of Microsoft of the site or the products contained therein.

2019 Microsoft Corporation. All rights reserved.

Microsoft and the trademarks listed at <https://www.microsoft.com/en-us/legal/intellectualproperty/Trademarks/Usage/General.aspx> are trademarks of the Microsoft group of companies. All other trademarks are property of their respective owners.

**Contents**
<!-- TOC -->

- [Media AI whiteboard design session student guide](#media-ai-whiteboard-design-session-student-guide)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Step 1: Review the customer case study](#step-1-review-the-customer-case-study)
    - [Customer situation](#customer-situation)
    - [Customer needs](#customer-needs)
    - [Customer objections](#customer-objections)
    - [Infographic for common scenarios](#infographic-for-common-scenarios)
      - [Video-on-demand digital media](#video-on-demand-digital-media)
      - [Keyword search/speech-to-text/OCR digital media](#keyword-searchspeech-to-textocr-digital-media)
  - [Step 2: Design a proof of concept solution](#step-2-design-a-proof-of-concept-solution)
  - [Step 3: Present the solution](#step-3-present-the-solution)
  - [Wrap-up](#wrap-up)
  - [Additional references](#additional-references)

<!-- /TOC -->

#  Media AI whiteboard design session student guide

## Abstract and learning objectives 

In this whiteboard design session, you will work in a group to look at the various options and services available to you in Azure to design a video-based learning solution that supports uploading and playback of videos, as well as using the more advanced AI capabilities of the platform.

By the end of the whiteboard design session you will be better able to design media applications including setup of the Video Indexer API, upload videos to Blob Storage to be encoded with Azure Video Indexer and integrate Video Indexer through Logic Apps and Azure Functions.

## Step 1: Review the customer case study 

**Outcome** 

Analyze your customer’s needs.

Timeframe: 15 minutes

Directions: With all participants in the session, the facilitator or SME presents an overview of the customer case study along with technical tips.
1.  Meet your table participants and trainer. 
2.  Read all of the directions for steps 1–3 in the student guide. 
3.  As a table team, review the following customer case study.

### Customer situation

Contoso Consulting is a mature consulting firm that has been in the IT Consulting business for over 30 years with clients all over the world. They have a history of mainly specializing in Microsoft technologies, but do have an established practice of offering Linux and OSS based consulting. The company has adapted to the ever-changing landscape of IT for a long time and has been involved in the training and adoption or transition process of many of its clients over the years. In addition to IT consulting services, Contoso provides on-site and video-based training to their larger clients. This involves creating custom training content and course agendas based on the client's needs and project being delivered.

Clients have always gotten a lot of value out of Contoso Consulting's training classes. Even though Contoso has trained the clients development and/or engineering teams, they are still brought back for future work since they have been able to deliver such great value over the years. Over the last couple years, clients have been asking them for more and more training courses. The referrals for training have been trickling down and coming from many of Contoso's smaller clients in addition to their larger clients. They have had to turn away a lot of smaller training contract opportunities due to the inability to create solutions affordable enough for their smaller clients.

Due to the efforts of their sales teams, Contoso has created many video-based training classes they have been able to deliver to clients using DVDs or MP4 files. While this enables them to reuse training content and deliver it in a more affordable and scalable manner, it just does not give them the full capabilities they would like. As the content ages, the DVDs or MP4s are no longer relevant and clients are solely purchasing individual courses. They have also used some of the available video hosting services on the market with only limited success. Everything they have tried so far has not quite offered the features and capabilities that an Enterprise needs for a good quality video streaming experience.

Because of this changing landscape of getting further into the IT training business, the CEO of Contoso Consulting, Jill Sampson, has decided to pursue the development of Contoso's own online, on-demand, video training service. Jill says, "I want to build the Netflix of IT training!" This new service will allow Contoso to easily resell subscriptions to their clients for all of their video training courses. This will include general courses that any client can be sold, to one-off custom courses built for specific clients based on their own specific IT infrastructure or custom software that Contoso has delivered to them.

Mary Bowman, the Director of IT Operations for Contoso, has seen some demos of the media and video services within the Microsoft Azure platform. However, she still has several questions to answer before making a final decision on the technology and platforms to use for implementation. The Contoso IT Development and Engineering teams do not have much experience with building and maintaining video streaming services, so they are a bit uncertain of what path to take with the design and architecture at this point. In initial conversations with the sales team, Mary has said, "We have a tremendous amount of Enterprise IT experience within our organization, we just need a little help figuring out the unknowns here."

### Customer needs

1.  The system must scale globally for their many clients all over the world, including new clients as Contoso grows their IT training business.

2.  They need to lessen the burden of content development by automatically generating video transcripts (video captions) and translate those into many other languages as well.

3.  Copy protection or DRM are desired to keep their IP from getting stolen.

4.  The ability to easily search through the content is needed so the service can be an easy to use lookup reference.

5.  The system needs to have an administration portal where new and updated content can be maintained by their internal staff.

6.  Use serverless compute where possible to minimize overall maintenance and overhead cost.

### Customer objections

1.  Is there a service to automate transcription of the videos? Manual transcription is costly.

2.  There are plenty of video encoding tools out there, but what does Azure offer to do this more easily?

3.  Do we need to hire translators to translate the transcripts into other languages?

4.  When we grow our library of content, how are we going to ensure the search capabilities stay responsive?

### Infographic for common scenarios

#### Video-on-demand digital media

![This diagram uses icons that are connected by arrows to illustrate common video-on-demand scenarios. The following icons are organized left to right and are connected by arrows that point right: Mezzanine Video Files (with a lock icon), Azure Blob Storage, Streaming Endpoint, Multi-Protocol Dynamic Packaging/Multi-DRM and Azure CDN. An Azure Encoder (Standard or Premium) icon points at the Azure Blob Storage icon with a bidirectional arrow. The Azure CDN arrow forks to Azure Media Player in Browser (top) and Azure Media Player in Mobile App (bottom). Both of these point to a Cloud DRM License/Key Delivery Server icon with arrows labeled Token. The Cloud DRM License/Key Delivery Server icon in turn points back to these icons with arrows labeled Licence/Key.](images/Whiteboarddesignsessiontrainerguide-MediaAIimages/media/image2.png "Common video-on-demand scenarios infographic")

#### Keyword search/speech-to-text/OCR digital media

![This diagram uses icons that are connected by arrows to illustrate keyword search/speech-to-text/OCR digital media. The following icons are organized left to right and are connected by arrows that point right: Source A/V File, Azure Blob Storage, Streaming Endpoint, Multi-Protocol Dynamic Packaging/Multi-DRM, Azure CDN and Azure Media Player. An Azure Encoder (Standard or Premium) icon points at the Azure Blob Storage icon with a bidirectional arrow and the Azure Blob Storage icon points at an Azure Media Indexer/OCR Media Processor icon. The Azure Media Indexer/OCR Media Processor icon points right with an arrow labeled TTML, WebVTT Keywords to an Azure Search icon, which in turn points at the Azure Media Player icon. A bidirectional arrow connects the Azure Media Player icon to the Azure Search icon through an icon labeled Web Apps.](images/Whiteboarddesignsessiontrainerguide-MediaAIimages/media/image3.png "Common keyword search or speech-to-text or OCR digital media infographic")


## Step 2: Design a proof of concept solution

**Outcome**

Design a solution and prepare to present the solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 60 minutes

**Business needs**

Directions: With all participants at your table, answer the following questions and list the answers on a flip chart:
1.  Who should you present this solution to? Who is your target customer audience? Who are the decision makers? 
2.  What customer business needs do you need to address with your solution?

**Design** 

Directions: With all participants at your table, respond to the following questions on a flip chart:

1.  What Azure Services are to be used to meet all of the customer needs?

2.  How would you answer the customers objections?

3.  Come up with a high-level architecture design for the media streaming solution that meets all of the customer's needs.

**Prepare**

Directions: With all participants at your table: 

1.  Identify any customer needs that are not addressed with the proposed solution. 
2.  Identify the benefits of your solution.
3.  Determine how you will respond to the customer’s objections. 

Prepare a 15-minute chalk-talk style presentation to the customer. 

## Step 3: Present the solution

**Outcome**
 
Present a solution to the target customer audience in a 15-minute chalk-talk format.

Timeframe: 30 minutes

**Presentation**

Directions:
1.  Pair with another table.
2.  One table is the Microsoft team and the other table is the customer.
3.  The Microsoft team presents their proposed solution to the customer.
4.  The customer makes one of the objections from the list of objections.
5.  The Microsoft team responds to the objection.
6.  The customer team gives feedback to the Microsoft team.
7.  Tables switch roles and repeat Steps 2–6.

##  Wrap-up

Timeframe: 15 minutes

-   Tables reconvene with the larger group to hear the facilitator/SME share the preferred solution for the case study.

##  Additional references

|    |            |
|----------|:-------------:|
| Build custom video AI workflow with Video Indexer and Logic Apps | <https://azure.microsoft.com/en-us/blog/build-custom-video-ai-workflows-with-video-indexer-and-logic-apps>|
| What is Azure Search?   | <https://docs.microsoft.com/en-us/azure/search/search-what-is-azure-search>  |
| Getting Started with the Video Indexer API    | <https://azure.microsoft.com/en-us/blog/gettingstartedwiththevideoindexerapi>  |
| Use Azure Video Indexer API    | <https://docs.microsoft.com/en-us/azure/cognitive-services/video-indexer/video-indexer-use-apis>   |
| Video Indexer API Signup |   <https://api-portal.videoindexer.ai/>   |
| Video Indexer Overview  | <https://docs.microsoft.com/en-us/azure/cognitive-services/video-indexer/video-indexer-overview>       |
| Web Apps Overview    | <https://docs.microsoft.com/en-us/azure/app-service/app-service-web-overview>  |
| What are Logic Apps?  |  <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-what-are-logic-apps>  |
| Introduction to Blob storage   |  <https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction>  |
| Azure Media Services overview  |  <https://docs.microsoft.com/en-us/azure/media-services/media-services-overview>   |
| Get started with delivering content on demand using .NET SDK  | <https://docs.microsoft.com/en-us/azure/media-services/media-services-dotnet-get-started>   |
| Deliver content to customers  | <https://docs.microsoft.com/en-us/azure/media-services/media-services-deliver-content-overview>    |
| Playback media with Media Player  | <https://docs.microsoft.com/en-us/azure/media-services/media-services-develop-video-players>  |
| Overview of the Azure Content Delivery Network (CDN)  | <https://docs.microsoft.com/en-us/azure/cdn/cdn-overview>    |
