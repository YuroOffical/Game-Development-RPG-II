drop database if exists RPGtheGame;
Create database RPGtheGame;
use RPGtheGame;

Create Table PClass(id_pclass varchar(10) primary key,pclass_category varchar(8), pclass_name varchar(15),p_vit int, p_str int, p_dex int, p_agi int, p_int int, p_wis int,p_buff varchar(11),p_img text,p_desc text);-- done
Create Table Enemy(id_enemy varchar(10) primary key,e_type varchar(1),e_name varchar(30),e_loc text,e_hp int,e_atk int, e_def int, e_armor int, e_speed int, e_special int, e_crit int ,e_img text); -- done can be expended
Create Table Skill(id_skill varchar(10) primary key, skill_name text,skill_desc text,skill_type varchar(5),skill_target varchar(2),skill_val int, skill_cost int, skill_cost_type varchar(2));-- done
Create table Effect(id_effect varchar(10), effect_name varchar(30), effect_desc text, effect_type varchar(5));-- done
Create Table Terrain(id_terrain varchar(10) primary key,t_name text,t_desc text,t_condition text,t_img text); -- done
Create Table Equipment (id_equip varchar(10), eq_name text, eq_desc varchar(40), eq_type varchar(10), eq_level int, eq_img text);-- done
Create Table Savefile (id_savefile varchar(10) primary key, save_teamname varchar(40), save_data_type varchar(10),save_data text);-- done

Create Table TeamInventory (id_savefile varchar(10),id_equip varchar(10),equipwho int); -- done
CreATe TaBLE EnemySkillset (id_enemy varchar(10),id_skill varchar(10),skillrate varchar(4),skillorder int); -- done can be expanded
create table Skilleffect (id_skill varchar(10), id_effect varchar(10), effect_target varchar(10), effect_duration int); -- done
Create Table ClassSkillset (id_pclass varchar(10),id_skill varchar(10),skilltype varchar(4), skillorder int); -- done

-- 4 Category (Survive, Attack, Magic, Balance)
-- Stats meaning 2 = 1.2x / 0 = 1.0x / -3 = 0.7x
insert into Savefile (id_savefile,save_teamname, save_data_type,save_data) values
('SF1-0','','team',''),
('SF1-1','','class',''),
('SF1-2','','class',''),
('SF1-3','','class',''),
('SF1-4','','class',''),
('SF2-0','','team',''),
('SF2-1','','class',''),
('SF2-2','','class',''),
('SF2-3','','class',''),
('SF2-4','','class',''),
('SF3-0','','team',''),
('SF3-1','','class',''),
('SF3-2','','class',''),
('SF3-3','','class',''),
('SF3-4','','class',''),
('SF4-0','','team',''),
('SF4-1','','class',''),
('SF4-2','','class',''),
('SF4-3','','class',''),
('SF4-4','','class','');

Insert into PClass(id_pclass,pclass_category,pclass_name,p_vit, p_str, p_dex, p_agi, p_int , p_wis ,p_img,p_desc) values
('C1S01','Survive','Knight',3,2,0,-1,-2,-2,'Knight.png','High Durability Tank'),-- health based
('C1S02','Survive','Paladin',4,1,0,-3,-1,-1,'Paladin.png','High Durability with healing spells'),-- magic based
('C1S03','Survive','Viking',3,3,1,-1,-3,-3,'Viking.png','High Defense and Attack'),-- health based
('C1S04','Survive','Giant',5,4,0,-5,-2,-2,'Giant.png','Extremely Durable, nearly invincible, but slow'),-- health based
('C1S05','Survive','Shielder',4,1,0,0,-2,-3,'Shielder.png','High Health points with buffing'),-- health based
('C2A01','Attack','Gunner',-2,-2,5,1,-2,1,'Gunner.png','High single target damage'),-- cash based
('C2A02','Attack','Archer',-2,-2,3,3,-1,-1,'Archer.png','High specialist Damage'),-- magic based
('C2A03','Attack','Assassin',-3,-4,5,3,0,-1,'Assassin.png','Glass Cannon'),-- magic based
('C2A04','Attack','Rider',1,-1,2,4,-4,-2,'Rider.png','High Attack Speed Unit'),-- health based
('C2A05','Attack','Jeep',-2,-2,3,5,-4,0,'Jeep.png','Great AoE Damage but high maintance'), -- cash based
('C3M01','Magic','Mage',-2,-2,-1,-1,3,3,'Mage.png','Magic User'),-- magic based
('C3M02','Magic','Priest',2,-3,-5,1,4,1,'Priest.png','The Typical healer'),-- magic based
('C3M03','Magic','Wizard',-4,-4,-2,1,5,4,'Wizard.png','The Better Magic User'),-- magic based
('C3M04','Magic','Necromancer',4,-2,-3,-2,2,1,'Necromancer.png','High Health while doing magic'),-- health based
('C3M05','Magic','Alchemist',-2,-3,-2,1,2,4,'Alchemist.png','Magic but capitalism'),-- cash based
('C4B01','Balance','Swordsman',-1,1,1,1,0,-2,'Swordsman.png','Basic of basics'),-- balance based
('C4B02','Balance','Lancer',0,1,2,0,-2,-1,'Lancer.png','Decent Stats, somewhat high durability'),-- balance based
('C4B03','Balance','Dwarf',2,2,1,-1,-2,-2,'Dwarf.png','A lesser viking'),-- balance based
('C4B04','Balance','Hero',1,-2,0,-2,2,1,'Hero.png','Blessed by the burning forge'),-- balance based
('C4B05','Balance','Stickboi',0,0,0,0,0,0,'Stickboi.png','He has a stick and hes armed');-- balance based

-- MP = Mana / HP = Health / CS = Cash / NN = None
-- SATK = Standard Attack / DAMG = Damaging Skill / HEAL = Healing Skill / STTS = Statues Skill
-- TS = Target Single Unit / TA = Target All Enemy / TF = Target Friend / TP = Target Party / TE = Target Everyone (not self) / TM = Target Self
-- SSANN01 = Skill Standard Attack No 1 (None-based)

Insert into Skill(id_skill, skill_name ,skill_desc,skill_type,skill_target,skill_val , skill_cost , skill_cost_type) values 

-- Standard Attack (no cost attack-based damage)
('SSANN01','Slash','Basic Melee Attack','SATK','TS',0,0,'NN'),
('SSANN02','Shot','Basic Ranged Attack','SATK','TS',0,0,'NN'),
('SSANN03','Magic Bullet','Basic Magic Attack','SATK','TS',0,0,'NN'),
('SSANN04','Flaming Slash','Basic Fire Attack','SATK','TS',0,0,'NN'),
('SSANN05','Guard','Guard Up','STTS','TS',0,0,'NN'),
-- Basic Skill (low mana skill)
('SBSMP01','Fireball','Cast a fireball, Deals Small Burn Damage','DAMG','TS',15,10,'MP'),
('SBSMP02','Iceball','Cast a iceball, deals small damage and slows','DAMG','TS',15,10,'MP'),
('SBSMP03','Poison Arrow','Deals Small damage and also poisons','DAMG','TS',15,10,'MP'),
('SBSMP04','Magic Missile','Cast a magic missile, Deals Medium damage','DAMG','TS',25,10,'MP'),

('SBSHP01','Great Slash','Slash with greater strength, Deals Medium damage for a bit of health','DAMG','TS',35,15,'HP'),
('SBSHP02','Strike','Charge and Strike, Deals Medium damage for health','DAMG','TS',45,25,'HP'),
('SBSHP03','Swipe','Swipes the Battlefield, Deals low damage to everyone for health','DAMG','TA',25,25,'HP'),
('SBSHP05','Slam','Slam your Shield Against the enemy, Deals Medium damage for a bit of health','DAMG','TS',30,10,'HP'),

('SBSCS01','Rapid Fire','Fire a Hail of bullets, Medium Damage','DAMG','TS',30,25,'CS'),
('SBSCS02','Suppresive Fire','Fire a Hail of bullets Randomly, Small Damage to Everyone','DAMG','TS',15,25,'CS'),

('SBSMP05','Heal','Basic Heal using magic','HEAL','TF',50,25,'MP'),
('SBSCS03','First Aid','Basic Heal using the power of Capitalism','HEAL','TF',40,25,'CS'),

('SBSMP06','Poison Cloud','Cast a cloud of poison','STTS','TS',0,15,'MP'),
('SBSMP07','Charge up','Hype self to deal heavy damage next turn','STTS','TF',0,20,'MP'),
('SBSMP08','Passion','A burning passion rises, you are blessed with fire','STTS','TM',0,20,'MP'),
('SBSMP09','Cold Heart','Unmoving Heart, you are blessed with ice','STTS','TM',0,20,'MP'),
('SBSMP10','Fortify','Fortify your body, Boost Defense','STTS','TM',0,20,'MP'),
('SBSMP11','Hasten Up','You hurried your mind, Boost Speed','STTS','TM',0,20,'MP'),
('SBSHP04','Sacrifice','flesh for power, Sacrifice health for a brief attack buff','STTS','TM',0,30,'HP'),
-- Advance Skill (Powerful Spell)
('SASMP01','Fire Tornado','Cast a Burning tornado, Deals Burning Damage to Everyone','DAMG','TA',15,60,'MP'),
('SASMP02','Hail Storm','Cast a hail storm, Deals Damage and slowness to Everyone ','DAMG','TA',15,60,'MP'),
('SASMP03','Blizzard','Howls a Blizzard from the sky, Deals damage and Freezes Everyone','DAMG','TA',15,80,'MP'),
('SASMP04','Acid Rain','Cast corroding rain, Applies Poison to everyone','STTS','TA',0,60,'MP'),
('SASMP05','Meteor','A Meteor is summoned, Deals Heavy Damage','DAMG','TA',120,100,'MP'),
('SASMP06','Tree of Life','Grows a tree that heals and regens everyone','HEAL','TP',30,60,'MP'),
('SASMP07','Creation','Creates new limbs from nothing, Massive Heal to one target','HEAL','TF',200,60,'MP'),
('SASMP08','Blessing','The Lord has Blessed, Everyone Regenerates Health','STTS','TP',0,60,'MP'),
('SASMP09','Burning Soul','Ignites the soul, Applies Fire Buff to Entire Party','STTS','TP',0,60,'MP'),
('SASMP10','Frozen Tundra','Area of Frozen Iceland, Applies Ice Buff to Entire Party','STTS','TP',0,60,'MP'),

('SASHP01','Heroic Swipe','Slash every enemy with a stereotipical hero pose, sacrifice health for damage','DAMG','TA',40,50,'HP'),
('SASHP02','Sacrificial Strike','A strike so strong it recoils back with half the force','DAMG','TS',150,75,'HP'),
('SASHP03','Summon Horde','Summons a horde of undead, deals damage while also poisoning them','DAMG','TA',10,60,'HP'),

('SASCS01','Hired Marksman','Hire a marksman to shot someone','DAMG','TA',120,100,'CS'),
('SASCS02','Expend Ammunition','Every Bullet will not go to waste, Damages to everyone','DAMG','TA',80,100,'CS'),
('SASCS03','Aimed Shot','Precision Strike on a vital organ, Heavy Damage','DAMG','TA',100,80,'CS'),

-- Unique Skill (only a few class or enemy will use)
('SUSMP01','Fallen Sky','The Sky is Falling','DAMG','TA',120,120,'MP'),
('SUSMP02','Great Wall','A Great wall stands before you','STTS','TP',0,120,'MP'),
('SUSMP03','Master Tactics','You are guided by a puppet of logistics','STTS','TP',0,120,'MP'),

('SUSHP01','There can be only one','Sacrifice your entire party for victory','STTS','TP',0,10,'HP');

-- BUFF Fire, Ice, Poison, Haste, Powerup, Charge, Fortify, Misty, Calculated
-- STATUES Rage Turtle Reckless Desperate
-- DEBUFF Burn Slow Frozen Poisoned Break target
-- UNIQUE void heroic finalstand

insert into Effect(id_effect , effect_name , effect_desc, effect_type) value
('B01','Fire','Blessed with Fire, Increase Damage and applies Burn Damage','lingr'),
('B02','Ice','Blessed with Ice, Applies Slow Damage','lingr'),
('B03','Poison','Blessed with Venom, Applies Poison','lingr'),
('B04','Haste','Blessed with Haste, Speed up','lingr'),
('B05','Power Up','Damage up','lingr'),
('B06','Charge','Damage Massively Up for 1 Turn','fixed'),
('B07','Fortify','Increase Defense','lingr'),
('B08','Misty','Increase Evasion','lingr'),
('B09','Calculated','Increase Criticality','lingr'),
('B10','Regen','Constant Healing','lingr'),
('S01','Rage','Increase damage decrease defense','lingr'),
('S02','Turtle','Increase defense decrease speed','lingr'),
('S03','Reckless','Increase speed decrease defense','lingr'),
('S04','Desperate','Increase Speed decrease damage','lingr'),
('D01','Burn','Damage over time and decrease defense','lingr'),
('D02','Slow','Halves Speed','lingr'),
('D03','Frozen','Halts Speed','lingr'),
('D04','Poisoned','Damage over time','lingr'),
('D05','Break','Decrease Defense','fixed'),
('D06','Targeted','Decrease Defense','fixed'),
('D07','Dread','Damage over time and reduce accuracy','lingr'),

('U01','Heroic','Buff All Stats','lingr'),
('U02','Last Stand','Buff All Stats and slight regen','lingr'),
('U03','Void','Debuff All Stats','lingr');


-- T = Target, M = Me/User , P = Party, E = Enemy Party, A = All(Everyone but caster)
insert into Skilleffect (id_skill , id_effect , effect_target, effect_duration ) value  
('SSANN04','D01','T',5), 
('SSANN05','B07','M',5), 
('SBSMP01','D01','T',10),
('SBSMP02','D02','T',10),
('SBSMP03','D04','T',10), 
('SBSMP06','D04','E',10), 
('SBSMP07','B06','M',10), 
('SBSMP08','B01','M',10), 
('SBSMP09','B02','M',10), 
('SBSMP10','B07','M',10), 
('SBSMP11','B04','M',10), 
('SASMP01','D01','E',15), 
('SASMP02','D02','E',15), 
('SASMP03','D03','E',15), 
('SASMP04','D04','E',15), 
('SASMP06','B10','P',15), 
('SASMP08','B10','P',25), 
('SASMP09','B01','P',20), 
('SASMP10','B02','P',20), 
('SASHP03','D04','E',10), 
('SUSMP02','B07','P',120), 
('SUSMP02','B10','P',20), 
('SUSMP03','B05','P',120), 
('SUSMP09','B05','P',120), 
('SUSMP09','B04','P',120), 
('SUSHP01','D01','A',999), 
('SUSHP01','D01','A',999), 
('SUSHP01','D07','A',999), 
('SUSHP01','U02','M',999);

-- type: 1 = weapon / 2 = helmet / 3 = armor / 4 = boots / 5 = auxillary
-- E101 = E = Equipment 1 = type of equipment 01 = number
insert into Equipment(id_equip , eq_name , eq_desc , eq_type , eq_level , eq_img ) values 
('E101','Rusty Sword','Its a Rusty Sword',1,1,'sword1.png'),
('E102','Killer Sniper','Its a Killer Sniper',1,1,'sniper1.png'),
('E103','RPG - 7','Its a RPG - 7',1,1,'rpg1.png'),
('E104','Sword of Blade','Its a Sword of Blade',1,2,'sword2.png'),
('E105','Bow','Its a Bow',1,1,'bow1.png'),
('E106','Crossbow','Its a Crossbow',1,2,'bow2.png'),
('E107','Katana','Its a Katana',1,1,'katana1.png'), 
('E108','Rocket launcher','Its a Rocket launcher',1,1,'rocket1.png'),
('E109','Battle fan','Its a Battle fan',1,1,'fan1.png'),
('E110','Bladed fan','Its a Bladed fan',1,2,'fan2.png'),
('E111','Bow and arrow','Its a Bow and arrow',1,3,'bow3.png'),
('E112','Boomerang','Its a Boomerang',1,1,'boomerang1.png'),
('E113','Great Sword','Its a Great Sword',1,3,'sword3.png'),
('E114','Axe of Blood','Its a Axe of Blood',1,1,'axe1.png'),
('E115','Holy Sword','Its a Holy Sword',1,4,'sword4.png'),
('E116','Boomerang Fire','Its a Boomerang Fire',1,2,'boomerang2.png'),
('E117','Magic staff','Its a Magic staff',1,1,'magic1.png'),
('E118','Magic Sword','Its a Magic Sword',1,5,'sword5.png'), -- sword done, sniper done, axe done, rocket done, bow done, boomerang done, rpg done
('E119','Boomerang Dagger','Its a Boomerang Dagger',1,3,'boomerang3.png'),
('E120','Bow of Pure','Its a Bow of Pure',1,4,'bow4.png'),
('E121','Whip Rocket','Its a Whip Rocket',1,2,'rocket2.png'),
('E122','Magic Axe','Its a Magic Axe',1,2,'axe2.png'),
('E123','Holy Sniper','Its a Holy Sniper',1,2,'sniper2.png'),
('E124','RPG - 075','Its a RPG - 075',1,2,'rpg2.png'),
('E125','Wand Bow','Its a Wand Bow',1,5,'bow5.png'), -- bow done
('E126','Magic Flame','Its a Magic Flame',1,2,'magic2.png'),
('E127','Throwing Rocket','Its a Throwing Rocket',1,3,'rocket3.png'),
('E128','Knive Fan','Its a Knive Fan',1,3,'fan3.png'),
('E129','Boomerang Flow','Its a Boomerang Flow',1,4,'boomerang4.png'),
('E130','Fan Launcher','Its a Fan Launcher',1,4,'fan4.png'),
('E131','RPG - 212','Its a RPG - 212',1,3,'rpg3.png'),
('E132','Bladed Axe','Its a Bladed Axe',1,3,'axe3.png'),
('E133','Swing Katana','Its a Swing Katana',1,2,'katana2.png'),
('E134','Magic Gun','Its a Magic Gun',1,3,'magic3.png'),
('E135','Rocket Flier','Its a Rocket Flier',1,4,'rocket4.png'),
('E136','Boomerang Blade','Its a Boomerang Blade',1,5,'boomerang5.png'), -- boomerang done
('E137','Katana Launcher','Its a Katana Launcer',1,3,'katana3.png'),
('E138','Fan Magic','Its a Fan Magic',1,5,'fan5.png'), -- fan done
('E139','Crescent Axe','Its a Crescent Axe',1,4,'axe4.png'),
('E140','Batlle Rocket','Its a Batlle Rocket',1,5,'rocket5.png'), -- rocket done
('E141','Divine Sniper','Its a Divine Sniper',1,3,'sniper3.png'),
('E142','Katana Magic','Its a Katana Magic',1,4,'katana4.png'),
('E143','RPG Launcher','Its a RPG Launcher',1,4,'rpg4.png'),
('E144','Magic Launcher','Its a Magic Launcher',1,4,'magic4.png'),
('E145','Tini Axe','Its a Tiny Axe',1,5,'axe5.png'), -- axe  done
('E146','Sniper Launcher','Its a Sniper Launcher',1,4,'sniper4.png'),
('E147','RPG - 001','Its a RPG - 001',1,5,'rpg5.png'), -- rpg done
('E148','Shield Katana','Its a Shield Katana',1,5,'katana5.png'), -- katana done
('E149','Magic Spark','Its a Magic Spark',1,5,'magic5.png'), -- magic done
('E150','Shield Sniper','Its a Shield Sniper',1,5,'sniper5.png'), -- sniper done
-- type: 1 = weapon / 2 = helmet / 3 = armor / 4 = boots / 5 = auxillary
-- helmet
('E201','Netherite  Helmet','Its a Netherite  Helmet', 2,1,'helmet1.png'),
('E202','Iron Helmet','Its a Iron Helmet', 2,2,'helmet2.png'),
('E203','Gold Helmet','Its a Gold Helmet', 2,3,'helmet3.png'),
('E204','Wooden Helmet','Its a Wooden Helmet', 2,4,'helmet4.png'),
('E205','Diamond Helmet','Its a Diamond Helmet', 2,5,'helmet5.png'),
-- armor
('E301','Netherite  Armor','Its a Netherite  Armor', 3,1,'armor1.png'),
('E302','Iron Armor','Its a Iron Armor', 3,2,'armor2.png'),
('E303','Gold Armor','Its a Gold Armor', 3,3,'armor3.png'),
('E304','Wooden Armor','Its a Wooden Armor', 3,4,'armor4.png'),
('E305','Diamond Armor','Its a Diamond Armor', 3,5,'armor5.png'),
-- boots
('E401','Leather Boots','Its a Leather Boots', 4,1,'boots1.png'),
('E402','Chainmail Boots','Its a Chainmail Boots', 4,2,'boots2.png'),
('E403','Iron Boots','Its a Iron Boots', 4,3,'boots3.png'),
('E404','Diamond Boots','Its a Diamond Boots', 4,4,'boots4.png'),
('E405','Netherite Boots','Its a Netherite Boots', 4,5,'boots5.png'),
-- auxillary
('E501','Diamond Ring','Its a Diamond Ring', 5,2,'ring1.png'),
('E502','Magic Necklace','Its a Magic Necklace', 5,3,'necklace1.png'),
('E503','Earing of Magic','Its a Earing of Magic', 5,5,'ear1.png'),
('E504','Magic Ring','Its a Magic Ring', 5,5,'ring2.png'),
('E505','Netherite Necklace','Its a Netherite Necklace', 5,3,'necklace2.png'),
('E506','Invisible Earing','Its a Invisible Earing', 5,1,'ear2.png'),
('E507','Fluently Bracelete','Its a Fluently Bracelete', 5,1,'bracelete1.png'),
('E508','Ring of Nether','Its a Ring of Nether', 5,4,'ring3.png'),
('E509','Mystical Ring','Its a Mystical Ring', 5,1,'ring4.png'),
('E510','Spark Bracelete','Its a Spark Bracelete', 5,4,'bracelete2.png');

-- N = Normal S = Skill
Insert into ClassSkillset (id_pclass ,id_skill ,skilltype ,skillorder) values
('C1S01','SSANN01','N',0), -- Melee
('C1S01','SBSMP10','S',1), -- Skill Fortify
('C1S01','SBSHP01','S',2), -- Skill Slash
('C1S01','SBSMP07','S',3), -- Skill Charge Up 

('C1S02','SSANN01','N',0), 
('C1S02','SBSMP05','S',1), 
('C1S02','SBSMP10','S',2), 
('C1S02','SBSMP07','S',3), 

('C1S03','SSANN01','N',0), 
('C1S03','SBSMP07','S',1), 
('C1S03','SBSHP01','S',2), 
('C1S03','SASHP01','S',3), 

('C1S04','SSANN01','N',0), 
('C1S04','SBSMP10','S',1), 
('C1S04','SBSHP05','S',2), 
('C1S04','SBSMP07','S',3), 

('C1S05','SSANN01','N',0), 
('C1S05','SBSMP10','S',1), 
('C1S05','SBSHP05','S',2), 
('C1S05','SBSMP05','S',3), 

('C2A01','SSANN02','N',0), 
('C2A01','SBSMP10','S',1), 
('C2A01','SASCS01','S',2), 
('C2A01','SBSMP07','S',3), 

('C2A02','SSANN02','N',0), 
('C2A02','SBSMP02','S',1), 
('C2A02','SBSMP10','S',2), 
('C2A02','SBSMP07','S',3), 

('C2A03','SSANN01','N',0), 
('C2A03','SBSMP07','S',1), 
('C2A03','SBSHP01','S',2), 
('C2A03','SASCS03','S',3), 

('C2A04','SSANN01','N',0), 
('C2A04','SBSMP10','S',1), 
('C2A04','SBSHP05','S',2), 
('C2A04','SBSMP07','S',3), 

('C2A05','SSANN02','N',0), 
('C2A05','SBSCS02','S',1), 
('C2A05','SASCS02','S',2), 
('C2A05','SBSMP10','S',3), 

('C3M01','SSANN03','N',0), 
('C3M01','SBSMP01','S',1), 
('C3M01','SASCS01','S',2), 
('C3M01','SASMP02','S',3), 

('C3M02','SSANN03','N',0), 
('C3M02','SBSMP05','S',1), 
('C3M02','SBSMP10','S',2), 
('C3M02','SASMP06','S',3), 

('C3M03','SSANN03','N',0), 
('C3M03','SBSMP01','S',1), 
('C3M03','SBSHP01','S',2), 
('C3M03','SASMP03','S',3), 

('C3M04','SSANN01','N',0), 
('C3M04','SBSMP06','S',1), 
('C3M04','SASHP03','S',2), 
('C3M04','SBSMP05','S',3), 

('C3M05','SSANN03','N',0), 
('C3M05','SBSCS02','S',1), 
('C3M05','SBSMP04','S',2), 
('C3M05','SBSMP11','S',3), 

('C4B01','SSANN01','N',0), 
('C4B01','SBSHP01','S',1), 
('C4B01','SBSMP07','S',2), 
('C4B01','SBSHP04','S',3), 

('C4B02','SSANN01','N',0), 
('C4B02','SBSHP01','S',1), 
('C4B02','SASMP07','S',2), 
('C4B02','SBSMP07','S',3), 

('C4B03','SSANN01','N',0), 
('C4B03','SBSMP07','S',1), 
('C4B03','SBSHP01','S',2), 
('C4B03','SBSMP08','S',3), 

('C4B04','SSANN04','N',0), 
('C4B04','SASMP09','S',1), 
('C4B04','SASMP01','S',2), 
('C4B04','SASHP01','S',3), 

('C4B05','SSANN01','N',0), 
('C4B05','SBSMP07','S',1), 
('C4B05','SUSMP03','S',2), 
('C4B05','SUSHP01','S',3);

-- EE = Enemy Entity // N = Normal B = Boss E = Elite
Insert into Enemy(id_enemy,e_type,e_name,e_loc ,e_hp,e_atk , e_def , e_armor , e_speed, e_special, e_crit  ,e_img ) values
('EEN01','N','Slime','safe',100,10,0,0,100,10,10,'Slime.png'),
('EEN02','N','Angry Boi','low',100,10,0,0,100,10,10,'Angry.png'),
('EEN03','N','Ball','safe',100,10,0,0,100,10,10,'Ball.png'),
('EEN04','N','Bird','low',100,10,0,0,100,10,10,'Bird.png'),
('EEN05','N','Birb','safe',100,10,0,0,100,10,10,'Birb.png'),
('EEN06','N','Birdier','safe',100,10,0,0,100,10,10,'Bird2.png'),
('EEN07','N','Blocky','low',100,10,0,0,100,10,10,'Block.png'),
('EEN08','N','Bob','low',100,10,0,0,100,10,10,'Bob.png'),
('EEN09','N','Bomb','low',100,10,0,0,100,10,10,'Bomb.png'),
('EEN10','N','Cloud','med',100,10,0,0,100,10,10,'Cloud.png'),
('EEN11','N','Geomatry','low',100,10,0,0,100,10,10,'Geomatry.png'),
('EEN12','N','Glovy','',100,10,0,0,100,10,10,'Glove.png'),
('EEN13','N','Hand','med',100,10,0,0,100,10,10,'Hand.png'),
('EEN14','N','Paper','med',100,10,0,0,100,10,10,'Paper.png'),
('EEN15','N','Something','med',100,10,0,0,100,10,10,'Something.png'),
('EEN16','N','Somethinger','med',100,10,0,0,100,10,10,'Something2.png'),
('EEN17','N','Wat','med',100,10,0,0,100,10,10,'Wat.png'),
('EEN18','N','Wat?','low',100,10,0,0,100,10,10,'Something3.png'),
('EEN19','N','Spiky','low',100,10,0,0,100,10,10,'Spiky.png'),

('EEE01','E','Circle','high',300,10,0,0,100,10,10,'Circle.png'),
('EEE02','E','Halo','high',300,10,0,0,100,10,10,'Halo.png'),
('EEE03','E','Orb','high',300,10,0,0,100,10,10,'Orb.png'),
('EEE04','E','Star','high',300,10,0,0,100,10,10,'Star.png'),
('EEE05','E','Sunflower','high',300,10,0,0,100,10,10,'Sunflower.png'),

('EEB01','B','Abyss','boss',1000,20,10,10,200,20,20,'Abyss.png');


Insert into EnemySkillset (id_enemy,id_skill,skillrate,skillorder) values
('EEN01','SBSMP05',10,1),
('EEN02','SBSHP02',10,1),
('EEN03','SBSMP05',15,1),
('EEN04','SBSHP02',10,1),
('EEN05','SBSHP02',10,1),
('EEN06','SBSMP05',10,1),
('EEN07','SBSMP01',12,1),
('EEN08','SBSMP05',10,1),
('EEN09','SBSHP02',16,1),
('EEN10','SBSHP02',10,1),
('EEN11','SBSHP02',10,1),
('EEN12','SBSMP05',13,1),
('EEN13','SBSMP05',12,1),
('EEN14','SBSMP01',10,1),
('EEN15','SBSMP05',20,1),
('EEE01','SBSCS01',20,1),
('EEE02','SBSMP05',20,1),
('EEE03','SBSMP05',15,1),
('EEE04','SASMP09',15,1),
('EEE05','SASMP10',15,1),
('EEB01','SUSMP01',10,1),
('EEB01','SASHP03',20,2);

-- Terrain
-- low, med, high, boss, safe
insert into Terrain(id_terrain,t_name ,t_desc,t_condition,t_img ) values
('T01','Plains','Plaines Biome,low enemy','low','T-Plains.jpg'),
('T02','Squares','Grid Squares,low enemy','low','Squares.jpg'), 
('T03','Mats','Battle Mats,med enemy','med','Mats.jpg'),  
('T04','Woods','Entwoods, safe enemy','safe','Woods.jpg'), 
('T05','Forest','Mushroom Forests, high enemy','high','Forest1.jpg'), 
('T06','Swamps','Haunted Swamps, boss enemy','boss','Swamps.jpg'),  
('T07','Canyons','Canyons, med enemy','med','Canyons.jpg'),
('T08','Forest','Jiggle Forests,safe enemy','safe','Forest2.jpg'), 
('T09','Land','Shelter Land, safe enemy','safe','Land.jpg'), 
('T10','Places','Fantastical Places, safe enemy','safe','Places.jpg'), 
('T11','Island','Underneath Island, boss enemy','boss','Island2.jpg'), 
('T12','Island','Netherland Island, med enemy','med','Island3.jpg'), 
('T13','Woods','Infinity Woods, high enemy','high','Woods2.jpg'), 
('T14','Squares','Magnifienct Squares, boss enemy','boss','Squares.jpg'), 
('T15','Survival','Survival Swamps, low enemy','low','Survival.jpg'), 
('T16','Plains','Invisible Plains, high enemy','high','Plains.jpg'), 
('T17','Classic','Classic Battle, low enemy','low','T-Plains.jpg'), 
('T18','UnderNeath','UnderNeath Forest, med enemy','med','UnderNeath.jpg'), 
('T19','Survival','Survival Jungle, safe enemy','med','Survival2.jpg'), 
('T20','Jungle','Jungle Park, low enemy','low','Jungle.jpg'),
('T99','???','???', 'Run, Danger','godhelpus.jpg');

