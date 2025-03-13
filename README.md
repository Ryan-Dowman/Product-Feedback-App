![Group 20](https://github.com/user-attachments/assets/87300eec-d60b-42ad-bf18-a31300fc0f20)

# Product Feedback App

## Table of Contents
- [Introduction](#introduction)
- [Showcase](#showcase)
- [Room for Improvement](#room-for-improvement)
- [Notes](#notes)

## Introduction
A Full Stack Web App that functions as a forum for users of a system to post suggestions, issues, and potential changes for developers.

## Showcase
### Login Page / Registration Page
Powered by Microsoft Identity and a designated AuthDb managed with SQL Server Management Studio. The registration page also enables image upload for profile pictures to the api/images route that locally stores the images and creates GUID names to ensure no naming conflicts.

### Home Page
Including ordering and filtering functionality as well as the ability to upvote feedback posts directly from the home page.
![chrome_DFrt9lDKSW](https://github.com/user-attachments/assets/73e52fb3-8103-48a3-89c4-43261c72007d)

### Roadmap Page
Shows the status of each feedback post so that users can see the progress of each post (it excludes all posts that are of type Suggestion (the default type set on create))
![chrome_5LlR532m8D](https://github.com/user-attachments/assets/ea129a5f-05fc-4206-8f03-e78d225b7e33)

### Add Feedback Page / Edit Feedback Page
A simple form page that allows all users to create and own a feedback post. Includes a JS-powered custom dropdown. (NOTE: Status is not an option as I decided only admins should be able to set the status of a feedback post)
![chrome_5eOJFeNQgU](https://github.com/user-attachments/assets/32924cd0-b2e0-4cc2-91b0-a7b256e8ac3b)

Allows the editing and deleting of posts only if the currently signed-in user is the owner of the post or an admin.
![chrome_AGyYv2AdoA](https://github.com/user-attachments/assets/cdb3a2a0-9e33-49bf-ad6c-84081f4e93f6)

### View Feedback Page
The view feedback page enables all users to upvote and post comments or reply to current comments. Admins will be given the ability in the top right to change the status of the feedback post here. Admins and owners of comments can also delete their comments.
![chrome_bHJwhGCF8K](https://github.com/user-attachments/assets/59f60e9b-e031-46e7-967b-6d4b13dd0863)

## Room for Improvement
This project was quite extensive for both the front and back end; hence, I do not have many things I would want changed or added. However, there are still a few things, including:

* Admin panel for user CRUD operations and moderation: Not implemented due to the limitations of the Frontend Mentor design
* A superior mobile layout: Not implemented due to a low reward-to-time ratio
* Custom error pages

## Notes
The design utilised was a paid for asset from the Frontend Mentor website. Icons found in this project were from the Bootstrap Icons Library on Figma. (Link Below) 
https://www.figma.com/community/plugin/868341386266170307/bootstrap-icons
