
![Xalami Logo](/RepoImages/xalami-logo.png)
# Xalami
Nobody loves boilerplate, especially if feels like you rewrite it every time you start a new project. Xalami is a starting point for Xamarin Forms projects that includes a lot of elements you'll use in every project. It's also delicious!

Included in the project are such tasty goodies as:
- An emphasis on MVVM
- A simple localization framework based on .resx files
- A Navigation service which wraps a Xamarin Forms NavigationPage, support parameter passing, and navigation by View type or ViewModel type.
- Several useful XAML markup extensions
- An ItemsStack control
- And more!

## Getting Started with Xalami
Check out the [Getting Started](https://github.com/futurice/Xalami/wiki/Getting-Started) page on the wiki!

Or...

### Windows
...just dive in and [download it from the Visual Studio Marketplace](https://marketplace.visualstudio.com/vsgallery/026321a8-871e-49de-b129-196c6dad34c9).

You can also download it directly from inside the Visual Studio. Go to Tools -> Extension Manager -> Online, and search for Xalami. After you install it, when you go File -> New -> Project, Xalami will be in your list under Visual C# -> Cross-Platform.

### Mac OSX

...get it from [the Releases in the repo](https://github.com/futurice/Xalami/releases/download/v1.0/Xalami.XS.Addin.mpack).

We're working on getting an addin uploaded to the Xamarin Studio/VS for Mac addin gallery!

## Philosophy
Xalami is *lightweight* but *opinionated*. This means that we expect you to use the template as a starting point, and not a heavyweight framework that you must conform to. Xalami *does* however expect you to follow a few basic tenets:
- You app is based around an MVVM architecture
- You have a centralized navigation service
- You have one ViewModel per view, and a view corresponds roughly to a single "page" in your app
-  You use XAML to define your UI

## Contributing
### Guidelines
[TBD]

### Building
#### The Installer
The installer is made up of three projects:
- The `Xalami.TemplateGenerator` project which generates a small .exe which is responsible for converting the runnable project into something packageable into a .vsix or .mdpack IDE extension.
- The `Xalami.VsixInstaller` project, which generates a .vsix installer for Visual Studio.
- The `Xalami.XamarinStudioAddin` project which generates (or will, anyway) an .mdpack installer for Xamarin Studio.

In order to create a .vsix or .mdpack, first the `Xalami.TemplateGenerator` project must be built:
- `git clone ` the repository.
- Right-click the `Xalami.TemplateGenerator` project and select `Build`. This will create a Xalami.TemplateGenerator.exe, which will be copied into the `TemplateGeneratorTool` under the repository's root directory.
- Then, simply right-click the project which builds the extension you're interested in and select `Build`!


## Credits
Much love to [Futurice](http://futurice.com/) for making this possible.

Proudly sponsored by Futurice's Spice Program

[![Sponsored](https://img.shields.io/badge/chilicorn-sponsored-brightgreen.svg?logo=data%3Aimage%2Fpng%3Bbase64%2CiVBORw0KGgoAAAANSUhEUgAAAA4AAAAPCAMAAADjyg5GAAABqlBMVEUAAAAzmTM3pEn%2FSTGhVSY4ZD43STdOXk5lSGAyhz41iz8xkz2HUCWFFhTFFRUzZDvbIB00Zzoyfj9zlHY0ZzmMfY0ydT0zjj92l3qjeR3dNSkoZp4ykEAzjT8ylUBlgj0yiT0ymECkwKjWqAyjuqcghpUykD%2BUQCKoQyAHb%2BgylkAyl0EynkEzmkA0mUA3mj86oUg7oUo8n0k%2FS%2Bw%2Fo0xBnE5BpU9Br0ZKo1ZLmFZOjEhesGljuzllqW50tH14aS14qm17mX9%2Bx4GAgUCEx02JySqOvpSXvI%2BYvp2orqmpzeGrQh%2Bsr6yssa2ttK6v0bKxMBy01bm4zLu5yry7yb29x77BzMPCxsLEzMXFxsXGx8fI3PLJ08vKysrKy8rL2s3MzczOH8LR0dHW19bX19fZ2dna2trc3Nzd3d3d3t3f39%2FgtZTg4ODi4uLj4%2BPlGxLl5eXm5ubnRzPn5%2Bfo6Ojp6enqfmzq6urr6%2Bvt7e3t7u3uDwvugwbu7u7v6Obv8fDz8%2FP09PT2igP29vb4%2BPj6y376%2Bu%2F7%2Bfv9%2Ff39%2Fv3%2BkAH%2FAwf%2FtwD%2F9wCyh1KfAAAAKXRSTlMABQ4VGykqLjVCTVNgdXuHj5Kaq62vt77ExNPX2%2Bju8vX6%2Bvr7%2FP7%2B%2FiiUMfUAAADTSURBVAjXBcFRTsIwHAfgX%2FtvOyjdYDUsRkFjTIwkPvjiOTyX9%2FAIJt7BF570BopEdHOOstHS%2BX0s439RGwnfuB5gSFOZAgDqjQOBivtGkCc7j%2B2e8XNzefWSu%2BsZUD1QfoTq0y6mZsUSvIkRoGYnHu6Yc63pDCjiSNE2kYLdCUAWVmK4zsxzO%2BQQFxNs5b479NHXopkbWX9U3PAwWAVSY%2FpZf1udQ7rfUpQ1CzurDPpwo16Ff2cMWjuFHX9qCV0Y0Ok4Jvh63IABUNnktl%2B6sgP%2BARIxSrT%2FMhLlAAAAAElFTkSuQmCC)](http://spiceprogram.org/oss-sponsorship)

Just as much love to the [Pepperoni App Kit](https://github.com/futurice/pepperoni-app-kit) which served as this project's inspiration.
