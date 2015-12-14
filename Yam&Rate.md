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

-   **Edit Restaurant view** – the same as Add New Restaurant, but it will have the input fields already filled in. Again when edits submitted – it redirects to the ‘Nearby Restaurants’ view;

-   **Restaurant Details view** – similar as the above, but instead of TextBoxes, it will have TextBlocks displaying the content. We can implement the Swipe left and right to navigate to nearby restaurants;

-   **See All Restaurants view** – listing all the restaurants or all the results from a search. When Tapped / Clicked on a restaurant it shows the ‘Restaurant Details’ view. When double touch is performed, it redirects to “Edit Restaurant” view;

-   We can also implement one more **gesture** globally (active in each of the views). Long press can redirect always to the ‘Initial’ view;

#### Looking at the requirements for the app:

-   The app is creative and is not the regular useless chat or blog;

-   The app has real value for the end user;

-   I still don’t know what ‘Custom Views’ are;

-   We will embed animations – it can be something simple. We can think of it later, when we already have some functionality and views;

-   We will use the Camera and GPS of the device. We can also use the Accelerometer to put the app in a background mode. We can also think of one more API to use;

-   We can use SQLite to store information locally and use it when the device is off-line. We should think more about that. We can load the whole DB in the user’s device and check for changes when there is connection to the internet. I am still not aware how we can implement this…

-   We will consume remote data from Backend services or from Parse.com. We still need to use Dropbox to store images there;

-   We can have sending and receiving data as a Background task as we talked. It also looks a bit complicated for now;

-   We will have touch friendly UI;

-   We will use notifications when user is trying to log in, register, add and edit restaurant. We can also use notifications to say to user that the app is in off-line or on-line mode. We can Notify user when the content (SQLite file) is updated and when information has been send to DB;

-   As described above, we already use 4 gestures

    -   double touch – on a map marker and on a restaurant in the list view;

    -   long touch – to redirect to the initial view;

    -   swipe – to navigate between Restaurant Details views;

    -   pinch – build in (or not) in the map;

-   Our code will be of high quality because we are very good hackers ;

-   We will validate user input;

-   We will handle network and other issues as appropriate;
