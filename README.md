Yam&Rate Application Structure
==============================

### Mobile Applications for Windows - Creating Line-of-business (LoB) Application

We will develop a Windows Universal (Windows 10) app.

#### The application will offer a rating system for restaurants. It will consist of the sequent features for end users:

-   Create accounts. User accounts will have:

    -   Email (provided also as username);

    -   Pass (saved in our storage as hashed pass);

    -   List of ratings (votes). Each user will be allowed to rate each restaurant only once;

-   Create restaurant profile. Each profile will have:

    -   Photo (or photos), made and shared by user(s);

    -   Name;

    -   Short description;

    -   List of main specialties for the restaurant – limited to five, for example;

    -   Rating – calculated as an average from the individual user’s ratings;

    -   GPS coordinates (longitude and latitude);

    -   Category of the restaurant;

-   Edit restaurant profile. Users will be able to edit profiles;

-   See restaurant profile information;

#### As for that, we will have to implement at least the sequent XAML views:

-   **Initial view**, providing links to ‘Log In / Register’ view, ‘Nearby Restaurants’ view, ‘Add New Restaurant’ view, ‘See All Restaurants’ view. It should also have a search field which results should be rendered in ‘See All Restaurants’ view;

-   **Log In / Register view** when successfully registered / logged it redirects to the Initial view;

-   **Nearby Restaurants view** – a map view displaying the current location of the user and the restaurants around. We should think of having sort description of a restaurant, when user taps the sign of the specific place. We can also use double touch on the map marker to redirect to the ‘Restaurant Details’ view;

-   **Add New Restaurant view** – It will have textboxes for Name, Category, Short description of the restaurant. It will also have five textboxes for the special dishes in the menu. It will have slider for rating and button ‘Take Photo’ that will activate the device camera. The coordinates will be send automatically according to the location of the user. When finished Add, it redirects to the ‘Nearby Restaurants’ view;

-   **Restaurant Details view** – similar as the above, but instead of TextBoxes, it will have TextBlocks displaying the content. We can implement the Swipe left and right to navigate to nearby restaurants. We will try to merge here the **Edit Restaurant view** – it will have the edited field converted to TextBox instead of TextBlock ready to be filled in. Edit submit will be lost focus on the specific field;

-   **See All Restaurants view** – listing all the restaurants or all the results from a search. When Tapped / Clicked on a restaurant it shows the ‘Restaurant Details’ view;

-   We can also implement one more **gesture** globally (active in each of the views). Double touch can redirect always to the ‘Initial’ view;

#### Looking at the requirements for the app:

-   The app is creative and is not the regular useless chat or blog;

-   The app has real value for the end user;

-   I still don’t know what ‘Custom Views’ are;

-   We will embed animations – it can be something simple. We can think of it later after implementing the functionality;

-   We will use the Camera and GPS of the device. We can also use the Accelerometer to put the app in a background mode. We will also use the device's Networkconnection;

-   We can use SQLite to store information locally and use it when the device is off-line. We should think more about that. We can load the whole DB (without the images) on the user’s device and check for changes when there is connection to the internet. We are still not sure how to implement this.

-   We will consume remote data from from Parse.com. We will store images there;

-   We can send and recive data as a Background task as we talked. It still looks a bit complicated for now;

-   We will have touch friendly UI;

-   We will use notifications when user is trying to log in, register, add and edit restaurant. We can also use notifications to say to user that the app is in off-line or on-line mode. We can Notify user when the content (SQLite file) is updated and when information has been send to DB;

-   As described above, we already use 4 gestures

    -   double touch – on a map marker;

    -   long touch – to redirect to the initial view;

    -   swipe – to navigate between Restaurant Details views;

    -   pinch – build in (or not) in the map;

-   Our code will be of high quality because we are very good hackers ;

-   We will validate user input;

-   We will handle network and other issues as appropriate;
