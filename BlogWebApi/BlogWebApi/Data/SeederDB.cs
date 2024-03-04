using BlogWebApi.Constants;
using BlogWebApi.Data.Entities;
using BlogWebApi.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogWebApi.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<AppEFContext>();
                context.Database.Migrate();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserEntity>>();
                var roleManager = scope.ServiceProvider
                    .GetRequiredService<RoleManager<RoleEntity>>();

                if (!context.Roles.Any())
                {
                    foreach (var role in Roles.All)
                    {
                        var result = roleManager.CreateAsync(new RoleEntity
                        {
                            Name = role
                        }).Result;
                    }
                }

                if (!context.Users.Any())
                {
                    var user = new UserEntity
                    {
                        UserName = "admin",
                        Email = "admin@gmail.com"
                    };
                    var result = userManager.CreateAsync(user, "admin").Result;
                    if (result.Succeeded)
                    {
                        result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
                    }
                }

                if (!context.Categories.Any())
                {
                    var categories = new List<CategoryEntity>
                    {
                        new CategoryEntity {
                            Id = 1,
                            Name = "Cars",
                            UrlSlug = "cars",
                            DateCreated = DateTime.UtcNow,
                            Description = "A car is a wheeled motor vehicle used for transportation. " +
                            "Most definitions of cars state that they run primarily on roads, seat one to eight people, " +
                            "have four tires, and mainly transport people rather than goods. Cars came into global use during the 20th century, " +
                            "and developed economies depend on them. The year 1886 is regarded as the birth year of the modern car " +
                            "when German inventor Karl Benz patented his Benz Patent-Motorwagen. Cars have controls for driving, parking, " +
                            "passenger comfort, and a variety of lights. Modern cars are typically powered by internal combustion engines fueled by " +
                            "gasoline or diesel, but alternatively by hybrid electric systems, pure electric systems, hydrogen fuel cells, and other alternative fuels."
                        },
                        new CategoryEntity
                        {
                            Id = 2,
                            Name = "VR",
                            UrlSlug = "vr",
                            DateCreated = DateTime.UtcNow,
                            Description = "Virtual Reality (VR) is a technology that immerses users in a computer-generated environment, simulating a " +
                            "realistic or imagined world. By wearing VR headsets or goggles, users are transported to a virtual space where they can interact " +
                            "with objects and experience events as if they were physically present. VR technology utilizes motion tracking, stereoscopic displays, " +
                            "and other sensory inputs to create a sense of presence and immersion. It is widely used in gaming, entertainment, education, training, " +
                            "healthcare, and other industries to provide immersive and engaging experiences."
                        }
                    };
                    context.Categories.AddRange(categories);
                    context.SaveChanges();
                }
                if (!context.Tags.Any())
                {
                    var tags = new List<TagEntity>
                    {
                        new TagEntity {
                            Id = 1,
                            Name = "APPLE",
                            UrlSlug = "apple",
                            DateCreated = DateTime.UtcNow,
                            Description = "Apple Inc. is a leading American multinational technology company known for its consumer electronics, " +
                            "software, and online services. Established in 1976 by Steve Jobs, Steve Wozniak, and Ronald Wayne, Apple is renowned for " +
                            "its innovative products such as the iPhone, iPad, Mac, and Apple Watch. The company's ecosystem includes software like macOS, " +
                            "iOS, and services like iCloud, iTunes, and the App Store. With a reputation for design excellence and user-friendly interfaces, " +
                            "Apple has become one of the most valuable companies globally."
                        },
                        new TagEntity {
                            Id = 2,
                            Name = "ELECTRIC CARS",
                            UrlSlug = "electric-cars",
                            DateCreated = DateTime.UtcNow,
                            Description = "Electric cars are vehicles powered by electric motors and rechargeable batteries instead of traditional " +
                            "gasoline engines. They produce zero tailpipe emissions, offering environmental benefits and lower operating costs. While " +
                            "they still face challenges such as limited driving range and charging infrastructure, advances in technology and growing " +
                            "environmental awareness have fueled their popularity. Leading manufacturers like Tesla, Nissan, and Chevrolet offer various " +
                            "electric car models to meet diverse consumer needs."
                        },
                        new TagEntity
                        {
                            Id = 3,
                            Name = "VISION PRO",
                            UrlSlug = "vision_pro",
                            DateCreated = DateTime.UtcNow,
                            Description = "Apple Vision Pro is a spatial computer that blends digital content and apps into your physical space, and lets " +
                            "you navigate using your eyes, hands, and voice. With visionOS on Apple Vision Pro, you can use built-in apps like Apple TV , " +
                            "Safari , and Photos , transform your space with Environments, connect with others in FaceTime calls, and download great " +
                            "third-party apps from the App Store ."
                        }
                    };
                    context.Tags.AddRange(tags);
                    context.SaveChanges();
                }
                if (!context.Posts.Any())
                {
                    var posts = new List<PostEntity>
                    {
                        new PostEntity {
                            Id = 1,
                            Title = "RIP to the Apple Car, we hardly knew ye",
                            ShortDescription = "Apple’s decision to kill its secretive car project is a reflection of the harsh reality confronting electric " +
                            "and autonomous vehicles across the globe.",
                            Meta = "Electric cars, Apple",
                            UrlSlug = "rip-apple-car-we-hardly-knew-ye",
                            Published = true,
                            PostedOn = DateTime.UtcNow,
                            DateCreated = DateTime.UtcNow,
                            Category = context.Categories.First(x => x.Id == 1),
                            Description = "In 2017, Apple CEO Tim Cook confirmed what the global auto industry had long feared: the tech giant was working on a " +
                            "driverless car.\r\n\r\n“We’re focusing on autonomous systems. And clearly, one purpose of autonomous systems are self-driving cars,” " +
                            "Cook said in an interview with Bloomberg. “And we sort of see it as the mother of all AI projects. It’s probably one of the most difficult " +
                            "AI projects actually to work on and so autonomy is something that’s incredibly exciting for us, but we’ll see where it takes us.”\r\n\r\n" +
                            "Where it took Apple was essentially nowhere. On Tuesday, Bloomberg’s Mark Gurman confirmed that Apple was scuttling its secretive car project, " +
                            "with most of the team’s workers moving over to generative AI initiatives. Others would likely be laid off. Presumedly the car would be mothballed " +
                            "alongside other never-to-be products like the Apple TV and the Paladin.\r\n\r\nNearly a decade after first launching Project Titan, Apple is back where " +
                            "it started. It remains a nebulous, mysterious object to the bitter end, never really ever taking shape beyond the rough, embarrassed imaginings " +
                            "of the company’s most devoted fan base. And while it seems to have been a mostly wasted effort, the decision to pull the plug is being celebrated " +
                            "by potential competitors and, most of all, investors.\r\n\r\nWall Street was always skeptical of Apple’s vehicular dalliances, viewing it as an " +
                            "expensive distraction with more pitfalls than positives. Wedbush’s Dan Ives congratulated Apple for “ripping the band-aid off,” arguing it was " +
                            "“clearly the right move for Cook & Co. moving forward.” Morgan Stanley’s analysts praised the company for “focusing on what matters” and “exhibiting " +
                            "cost discipline.”\r\n\r\nThe implication isn’t that an Apple car wouldn’t matter in the grand scheme of things, but just that the company was " +
                            "better off focusing on more buzzworthy projects more in its wheelhouse, like AI.\r\n\r\nIt’s not a surprise that the demise of the Apple " +
                            "car coincides with a bleaker outlook for electric and autonomous vehicles. After years of promising that battery-powered, driverless " +
                            "vehicles would soon dominate personal transportation, the industry is going through serious growing pains. Investments are being reined " +
                            "in, factories put on pause, and model lineups canceled. Pure EV plays like Rivian and Lucid, companies that are exclusively making plug-in " +
                            "vehicles, are struggling to find customers as most people are looking for something more affordable — or hedging their bets with a hybrid.\r\n\r\n" +
                            "If Apple were to suddenly parachute onto this stage, the tech giant would likely confront similar headwinds, said Sam Abuelsamid, principal analyst " +
                            "at Guidehouse Insights.\r\n\r\n“Affordability is an increasing problem and since Apple isn’t going to want to sell an entry level EV, that leaves " +
                            "them with an increasingly tight premium market,” he said. “If Lucid and Rivian can’t find a way to sell expensive EVs with products as good as " +
                            "they have, it’s going to be tough for another newcomer like Apple.”\r\n\r\nA fully autonomous vehicle would have been just as difficult, if not " +
                            "more so. Just look at the increasingly fraught adventures of the various robotaxi companies in California, where cars are crashing into pedestrians, " +
                            "buses, and bicyclists with increasing frequency. Apple, always incredibly image conscious, would do best to avoid similar headlines.\r\n\r\nTo be sure, " +
                            "developing a suite of hardware and software needed to enable a car to drive itself always seemed more aligned with Apple’s capabilities " +
                            "than building a whole damn car from scratch. And the company clearly made some progress there, operating a modest-sized fleet of vehicles " +
                            "in California and even releasing a hilariously sparse, seven-page safety report to federal regulators.\r\n\r\nBut Apple never really caught " +
                            "up to its rivals, who had been working on this problem longer and with more resources to spend. It never obtained a permit from California " +
                            "regulators to operate its vehicles without safety drivers in the front seat. And the size of the company’s fleet stayed relatively flat. " +
                            "But Apple stuck to it. Last year, the company bested Waymo and Cruise in the rate of increase of miles traveled by its autonomous vehicles, " +
                            "The Washington Post reported.\r\n\r\nSure, Apple can retrofit several dozen cars with sensors and software, and make them drive themselves. " +
                            "A lot of companies can do that. Over three dozen companies have self-driving cars on the road in California. It’s not too hard to find a " +
                            "bunch of Toyota Highlanders and stick some cameras and lidar on them.\r\n\r\nBut it’s the next step, and the step after that, that proved too " +
                            "daunting. Given the company’s control freakiness, the only way Apple could make this work was likely as a closed robotaxi ecosystem, " +
                            "perhaps as a monthly subscription service. But that’s a business model that’s yet to be proven. Even Elon Musk can’t seem to make the " +
                            "math — or technology — work.\r\n\r\nNone of its rumored deals ever panned out. It was going to partner with BMW. No wait, it’s Volkswagen. " +
                            "Ah, actually how about Hyundai? Or Nissan? When none of the established automakers would sign up to build Apple’s car, it decided to pivot " +
                            "to software. It bought Drive.AI just days before the startup would have run out of cash, acquiring the small company’s small team of AV " +
                            "engineers. And it could never decide who exactly should be in charge of this whole operation.\r\n\r\nFinally, in January, the top executives " +
                            "gave Project Titan a mandate: put up or shut up. Bloomberg reported that self-driving cars were out, and Level 2+ ADAS — think Tesla’s " +
                            "Full Self-Driving or GM’s Super Cruise — was in. The launch date was pushed to 2028.\r\n\r\nOnly it wasn’t. Weeks later, the plug was " +
                            "pulled. The Apple car was toast. We’ll never know what we missed."
                        },
                        new PostEntity
                        {
                            Id = 2,
                            Title = "The Vision Pro isn’t destroying your eyes, but maybe get some eye drops",
                            ShortDescription = "Screens ruining your vision is a myth, but dry eye and digital eye strain are common side effects of using VR headsets.",
                            Meta = "Apple, Vision Pro",
                            UrlSlug = "The-Vision-Pro-isnt-destroying-your-eyes-but-maybe-get-some-eye-drops",
                            Published = true,
                            PostedOn = DateTime.UtcNow,
                            DateCreated = DateTime.UtcNow,
                            Category = context.Categories.First(X => X.Id == 2),
                            Description = "We’ve all heard that screens aren’t good for your eyes. So it might not be too surprising to hear that many Vision Pro " +
                            "users have complained about eye strain. (After all, the headset does use two 4K screens, one in front of each eyeball.) However, " +
                            "these are common complaints from overall VR usage and experts say it isn’t something to freak out over.\r\n\r\n“Despite what many " +
                            "people believe, sitting too close to the TV does not damage your eyes. Screens ruining your eyes is another myth,” says Dr. Arvind Saini, " +
                            "clinical spokesperson for the American Academy of Ophthalmology.\r\n\r\nIf you peruse VR subreddits, not just the Apple Vision Pro one, " +
                            "that can be hard to believe. You’ll often find people complaining that their eyes “hurt like hell,” are irritated or even bloodshot. " +
                            "However, Saini says that these are all temporary symptoms likely caused by people not blinking enough while using the devices. " +
                            "As for symptoms like dizziness and nausea, Saini says that’s because when you view an image in motion, it sends the same signals " +
                            "to your brain as if you were actually in motion — even if you’re standing still.\r\n\r\nEye strain can also be caused by " +
                            "something called the vergence-accommodation conflict. In the real world, when you look at an object, the focal point and physical " +
                            "distance to that object is the same. In VR, depth is simulated — so distance of your eye to the physical screen and the thing you’re " +
                            "focusing on in the virtual world can be mismatched. That causes your eye muscles to fatigue.\r\n\r\n“Although these symptoms can " +
                            "sometimes be uncomfortable, there is no scientific evidence to suggest that any digital screens, including a tool like VR devices, " +
                            "are harmful to eye health.”\r\n\r\nBut what about some of the more alarming VR headset claims involving redness and hemorrhaging " +
                            "in the eye? Saini says that those, too, are not dangerous to your vision. These are called subconjunctival hemorrhage, and while " +
                            "scary looking, they’re generally harmless and heal on their own. They can be caused by quick pressure changes (i.e., sneezing or " +
                            "coughing), which can pop the capillaries in your eyes, or by eye trauma.\r\n\r\n“Screen use or VR use itself cannot cause subconjuctival " +
                            "hemorrhage,” says Saini. However, he says that VR (or other screen usage) can indirectly cause blood vessels to burst if " +
                            "you’re constantly rubbing your eyes to deal with screen-related dry eye.\r\n\r\nScreens — VR or otherwise — aren’t going " +
                            "anywhere. If anything, big tech seems increasingly convinced that AR is the future. Given that, the eye pain they cause " +
                            "can’t be ignored. So, VR companies have taken a relatively conservative approach in advising how people use their devices." +
                            "\r\n\r\nFor example, most VR headset makers warn that their devices are not for children under 13 years old. This is partly " +
                            "because they’re not designed for smaller bodies and also because children’s eyes are still developing. For example, " +
                            "Meta’s Quest compliance page notes that “Children’s bodies tend to be less developed, so their eyes, necks, backs and " +
                            "strength may not yet allow them to use Meta Quest comfortably or safely.” This is even though there isn’t conclusive " +
                            "evidence, or enough research done yet, to say whether using the headset negatively impacts children’s vision.\r\n\r\nBut " +
                            "even if your overall vision isn’t at stake, it doesn’t change the fact that VR can make your eyes hurt. There are, " +
                            "however, things you can do to mitigate that. A lot of it is common sense. Apple’s Vision Pro support page recommends " +
                            "easing into the device, taking breaks every 20-30 minutes when starting out. It also emphasizes getting the best possible " +
                            "fit. Meta’s compliance page says the same, adding that experts say children should be limited to two hours per day. " +
                            "Saini recommends following the 20-20-20 method. Every 20 minutes, you should take a 20 second break and look 20 feet off into " +
                            "the distance. And if all else fails, you can always invest in some eye drops."
                        }
                    };
                    context.Posts.AddRange(posts);
                    context.PostTags.AddRange(
                        new PostTagEntity { PostId = 1, TagId = 1 }, 
                        new PostTagEntity { PostId = 1, TagId = 2 },
                        new PostTagEntity { PostId = 2, TagId = 3 });
                    context.SaveChanges();
                }
            }
        }
    }
}
